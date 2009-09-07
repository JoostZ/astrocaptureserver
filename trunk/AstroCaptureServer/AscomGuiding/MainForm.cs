using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ASCOM.DriverAccess;

using ScopeServer;

namespace AscomGuiding
{
    public partial class MainForm : Form
    {
        private AscomDriver iDriver = null;
        private ScopeServer.GuidingServer iServer = null;
        private int _port = 4242;
 
        public MainForm()
        {
            InitializeComponent();
            txtPort.Text  = _port.ToString();
            SetupServer();
        }

        private void btnSelectDriver_Click(object sender, EventArgs e)
        {

            string ProgId = Telescope.Choose("");
            if (iDriver != null)
            {
                iDriver.Dispose();
            }
            iDriver = new AscomDriver(ProgId);
            iServer.Driver = iDriver;
            btnSetup.Enabled = true;

            txtSelectedDriver.Text = iDriver.Name;

            ShowPulseGuiding();
            
        }

        private void ShowPulseGuiding()
        {
            double rate = iDriver.GuideRateAscension;
            if (rate == 0.0)
            {
                txtRaPulseRate.Text = "";
            }
            else
            {
                txtRaPulseRate.Text = ((int)(1.0 / (rate * 3.6))).ToString();
            }

            rate = iDriver.GuideRateDeclination;
            if (rate == 0.0)
            {
                txtDecPulseRate.Text = "";
            }
            else
            {
                txtDecPulseRate.Text = ((int)(1.0 / (rate * 3.6))).ToString();
            }
            
        }

        private void SetupServer()
        {
            iServer = new ScopeServer.GuidingServer();
            iServer.Port = _port;
            iServer.Driver = iDriver;

            iServer.OnStatusMessage += new ScopeServer.GuidingServer.StatusMessageHandler(DoError);
            iServer.OnMessageReceived += new ScopeServer.GuidingServer.ReceivedMessageHandler(ShowMessage);
            iServer.Start();
        }

        private delegate void SetTextCallback(String result);
        public void DoError(String msg)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(DoError);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                txtError.Text = msg;
            }
        }

        public void ShowMessage(String msg)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(ShowMessage);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                txtMessage.Text = msg;
            }
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            iDriver.Setup();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iDriver != null)
            {
                iDriver.Dispose();
            }
        }

        private void btnChangePort_Click(object sender, EventArgs e)
        {
            PortDialog dialog = new PortDialog();
            dialog.Port = _port;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.Port != _port && iServer != null)
                {
                    _port = dialog.Port;
                    txtPort.Text = _port.ToString();
                    iServer.Start();
                }
            }

        }
    }
}
