using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/**
 * @mainpage
 * GuidingServer is a class that implements a generic server for wxAstroCapture. It receives
 * the messages sent from that program, parses it and calls a driver to perform the movement
 * around the axes.
 * 
 * The messages received from wxAstroCapture are 21 bytes long and look like this:
 * 
 * MddddddddRsddddDsdddd
 * 
 * M, R and D are the start of fields in the message
 *   - M is the start of the message. The 8 digits following specify
 *     the time in miliseconds from the start of wxAstroCapture to
 *     the sending of the message.
 *   - R is the required duration, in miliseconds, of the rotation around the Right Ascension
 *     axis. The 's' the sign indicates the direction of the rotation. The field has always 
 *     exactly 4 digits
 *   - D is the required duration, in miliseconds, of the rotation around the Declination
 *     axis. The 's' the sign indicates the direction of the rotation. The field has always 
 *     exactly 4 digits
 */

/// <summary>
/// namespace for the whole project
/// </summary>
namespace AstroCaptureServer
{
    namespace Server
    {
        /// <summary>
        /// Server for wxAstroCapture
        /// </summary>
        public class GuidingServer
        {
            /// <summary>
            /// Delegate to pass strings back
            /// </summary>
            /// <param name="status">The string to pass on</param>
            public delegate void StatusMessageHandler(String status);

            /// <summary>
            /// Event handler for received message
            /// </summary>
            public event StatusMessageHandler OnMessageReceived = null;

            /// <summary>
            /// Event handler for Status
            /// </summary>
            public event StatusMessageHandler OnStatusMessage = null;

            /// <summary>
            /// Event handler for Errors
            /// </summary>
            public event StatusMessageHandler OnErrorMessage = null;


            private int iServerPort = 5618;

            private ITelescopeDriver iDriver = null;

            /// <summary>
            /// Constructor
            /// </summary>
            public GuidingServer()
            {
                
                    if (OnStatusMessage != null)
                    {
                        OnStatusMessage("Starting Server...");
                    }
                    
                    StartListener();
            }

            private void StartListener()
            {
                // Create a TCP/IP socket.
                iListener = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the local endpoint and listen for incoming connections.
                try
                {
                    iListener.Bind(new IPEndPoint(IPAddress.Any, iServerPort));
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

            /// <summary>
            /// The driver to be used in this server
            /// </summary>
            public ITelescopeDriver Driver
            {
                get
                {
                    return iDriver;
                }
                set
                {
                    lock (this)
                    {
                        iDriver = value;
                    }
                }
            }

            /// <summary>
            /// The TCP/IP port to which the server will listen
            /// </summary>
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

            private Socket iListener = null;
            private Socket iChannel = null;

            public void ReStart()
            {
                lock (this)
                {
                    // Close possible previous listener
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

                    StartListener();
                }
            }

            private void StartListening()
            {
                // Start an asynchronous socket to listen for connections.
                if (OnStatusMessage != null)
                {
                    OnStatusMessage("Waiting for a connection...");
                }

                if (OnMessageReceived != null)
                {
                    OnMessageReceived("");
                }
                iTimeSet = false;
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

            class Message
            {
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

            DateTime iClientTime;
            bool iTimeSet = false;
            Int64 iThreadStart;

            public Int16 UpdatePeriod { get; set; }

            public delegate void DelayHandler(Int64 delay);
            public event DelayHandler OnDelayReceived;

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

                        Int64 delay = 0;
                        if (!iTimeSet)
                        {
                            iThreadStart = GetLocalMillis() - timeStamp;
                            iClientTime = DateTime.Now - TimeSpan.FromMilliseconds(timeStamp);
                            iTimeSet = true;
                            delay = 0;
                        }
                        else
                        {


                            Int32 timeDiff = (int)(DateTime.Now - iClientTime).TotalMilliseconds;
                            Int64 actualMessageTime = GetLocalMillis() - iThreadStart;
                            delay = actualMessageTime - timeStamp;
                        }

                        if (OnDelayReceived != null)
                        {
                            OnDelayReceived(delay);
                        }

                        if (delay > UpdatePeriod)
                        {
                            // 
                            iMessage.index = 0;
                            continue;
                        }
                    }
                    catch
                    {
                        // Message is malformed. Skip this message
                        iMessage.index = 0;
                        continue;
                    }

                    iMessage.index = 0;

                    if (iDriver != null)
                    {
                        iDriver.Pulse(raPuls, dePuls);
                    }
                }
                ReadBuffer(state);
            }



            Int64 GetLocalMillis()
            {
                return DateTime.Now.Ticks / 10000;
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
}