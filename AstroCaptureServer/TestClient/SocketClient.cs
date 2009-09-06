using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    public partial class SocketClient : Form
    {
        private TcpClient client;
        private BinaryWriter writer;
        private NetworkStream output;
        private DateTime startTime;

        public SocketClient()
        {
            InitializeComponent();
            startTime = DateTime.Now;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
            {

                StopClient();
            }
            else
            {

                StartClient();
            }

        }

        private void StopClient()
        {
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }

            if (output != null)
            {
                output.Close();
                output = null;
            }
            if (client != null)
            {
                client.Close();
                client = null;
            }
            btnDisConnect.Enabled = false; ;
        }

        private void StartClient()
        {
            if (client != null)
            {
                StopClient();
            }

            client = new TcpClient();
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = Int16.Parse(txtPort.Text);
            client.Connect(localAddr, port);
            output = client.GetStream();
            writer = new BinaryWriter(output);
            btnDisConnect.Enabled = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                StartClient();
            }

            StringBuilder builder = new StringBuilder();
            TimeSpan diff = DateTime.Now - startTime;
            Int16 raPulse = Convert.ToInt16(txtRATime.Text);
            Int16 decPulse = Convert.ToInt16(txtDETime.Text);

            builder.AppendFormat("M{0:00000000}", diff.TotalMilliseconds);
            builder.AppendFormat("R{0}{1:0000}", raPulse >= 0 ? '+' : '-', Math.Abs(raPulse));
            builder.AppendFormat("D{0}{1:0000}", decPulse >= 0 ? '+' : '-', Math.Abs(decPulse));
            String theMessage = builder.ToString();
            txtSending.Text = theMessage;
            writer.Write(theMessage);
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            AscomGuiding.PortDialog theDialog = new AscomGuiding.PortDialog();
            theDialog.Port = Int16.Parse(txtPort.Text);
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                String newPort = theDialog.Port.ToString();
                if (newPort != txtPort.Text)
                {
                    txtPort.Text = newPort;
                }
            }

        }
    }
}
