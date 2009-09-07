using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace ScopeServer
{
    /**
     * @brief
     * Server for wxAstroCapture
     */
    public class GuidingServer
    {
        public delegate void ReceivedMessageHandler(String result);
        public event ReceivedMessageHandler OnMessageReceived;

        public delegate void StatusMessageHandler(String status);
        public event StatusMessageHandler OnStatusMessage = null;
        public event StatusMessageHandler OnErrorMessage = null;

  
        private int iServerPort = 4242;

        private ITelescopeDriver _driver = null;

        public GuidingServer()
        {
            ;
        }

        public ITelescopeDriver Driver
        {
            get
            {
                return _driver;
            }
            set
            {
                lock (this)
                {
                    _driver = value;
                }
            }
        }

        public int Port
        {
            get
            {
                return iServerPort;
            }
            set
            {
                iServerPort = value;
            }
        }

        public void Start()
        {
            StartServer();
        }

        public void Stop()
        {
            
        }


        private Socket iListener = null;
        private Socket iChannel = null;

        private void StartServer()
        {
            // Establish the local endpoint for the socket.
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(localAddr, iServerPort);

            lock (this)
            {
                if (OnStatusMessage != null)
                {
                    OnStatusMessage("Starting Server...");
                }
                if (iChannel != null)
                {
                    iChannel.Shutdown(SocketShutdown.Both);
                    iChannel = null;
                }


                if (iListener != null)
                {
                    if (iListener.Connected)
                    {
                        iListener.Shutdown(SocketShutdown.Receive);
                    }
                    iListener.Close();
                    iListener = null;
                }

                // Create a TCP/IP socket.
                iListener = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.
                try
                {
                    iListener.Bind(localEndPoint);
                    iListener.Listen(100);


                    StartListening();


                }
                catch (Exception e)
                {
                    if (OnErrorMessage != null)
                    {
                        OnErrorMessage(e.ToString());
                    }
                }
            }
        }

        private void StartListening()
        {
            // Start an asynchronous socket to listen for connections.
            if (OnStatusMessage != null)
            {
                OnStatusMessage("Waiting for a connection...");
            }
            iListener.BeginAccept(
                new AsyncCallback(AcceptCallback),
                iListener);
        }

        private StateObject iState;
        private void AcceptCallback(IAsyncResult ar)
        {
            lock (this)
            {

                // Get the socket that handles the client request.
                Socket listener = (Socket)ar.AsyncState;
                if (listener != iListener)
                {
                    return;
                }
                iChannel = listener.EndAccept(ar);
                if (OnStatusMessage != null)
                {
                    OnStatusMessage("Connected");
                }

                // Create the state object.
                iState = new StateObject();
                iState.workSocket = iChannel;
                iChannel.BeginReceive(iState.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), iState);
            }

        }
        
        private const int msgLength = 21;
        private const int raOffset = 9;
        private const int decOffset = 15;

        class Message {
            public int index;
            public byte[] buffer;
            public Message()
            {
                index = 0;
                buffer = new byte[msgLength];
            }
        };

        Message iMessage = new Message();

            // 0123456789a123456789b1
            // M12345678Rs1234Ds1234 --> msgLen=21 chars
            // M<-Time->R+1234D+1234

            // The message contains 21 characters
            // M12345678 = Time in [ms] since start of client's thread
            // Rs1234    = Time in [ms] and direction of RA guiding pulse
            // Ds1234    = Time in [ms] and direction of DE guiding pulse

        private void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            if (handler.Connected)
            {

                try
                {
                    // Read data from the client socket. 
                    state.bytesRead = handler.EndReceive(ar);
                    state.index = 0;
                }
                catch (SocketException)
                {
                    // Probably closed at other side
                    state.bytesRead = 0;
                }
            }
            else
            {
                state.bytesRead = 0;
            }

            if (state.bytesRead == 0)
            {
                // channel has been closed
                lock (this)
                {
                    if (iChannel != null)
                    {
                        // The remote side hase closed the channel
                        // Go wait for a new connetion
                        iChannel = null;
                        StartListening();
                    }
                    // else we closed the channel. Nothing to do
                    return;
                }
            }

            while (state.index < state.bytesRead)
            {

                while (iMessage.index == 0)
                {
                    while (state.index < state.bytesRead && state.buffer[state.index++] != 'M')
                    {
                        ; // Intentionally nothing to do
                    }
                    if (state.index >= state.bytesRead)
                    {
                        ReadBuffer(state);
                        return;
                    }
                    else
                    {
                        iMessage.buffer[0] = (byte)'M';
                        iMessage.index = 1;
                    }
                }

                for (; iMessage.index < msgLength; ++iMessage.index)
                {
                    if (state.index >= state.bytesRead)
                    {
                        ReadBuffer(state);
                        return;
                    }
                    iMessage.buffer[iMessage.index] = state.buffer[state.index++];

                }

                if (OnMessageReceived != null)
                {
                    String received = Encoding.ASCII.GetString(iMessage.buffer);
                    OnMessageReceived(received);
                }

                // Verify the message is the right format
                if (iMessage.buffer[raOffset] != (char)'R' ||
                    iMessage.buffer[decOffset] != (char)'D')
                {
                    // Message is malformed. Skip this message
                    iMessage.index = 0;
                    continue;
                }
               

                Int16 raPuls;
                Int16 dePuls;
                try
                {
                    Int32 timeStamp = Convert.ToInt32(Encoding.ASCII.GetString(iMessage.buffer, 1, 8));
                    raPuls = Convert.ToInt16(Encoding.ASCII.GetString(iMessage.buffer, raOffset + 1, 5));
                    dePuls = Convert.ToInt16(Encoding.ASCII.GetString(iMessage.buffer, decOffset + 1, 5));
                }
                catch
                {
                    // Message is malformed. Skip this message
                    iMessage.index = 0;
                    continue;
                }

                iMessage.index = 0;

                if (_driver != null)
                {
                    _driver.Pulse(raPuls, dePuls);
                }
            }
            ReadBuffer(state);
        }



       

        private void ReadBuffer(StateObject aState)
        {
            aState.workSocket.BeginReceive(aState.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), aState);

        }

        
    }

    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        public int bytesRead = 0;
        public int index = 0;

    }
}
