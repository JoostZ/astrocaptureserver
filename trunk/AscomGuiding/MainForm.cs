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
        private AscomDriver _driver = null;
        private ScopeServer.GuidingServer iServer = null;
        private int _port = 4242;
 
        public MainForm()
        {
            InitializeComponent();
            SetupServer();
        }

        private void btnSelectDriver_Click(object sender, EventArgs e)
        {

            string ProgId = Telescope.Choose("");
            txtSelectedDriver.Text = ProgId;
            _driver = new AscomDriver(ProgId);
            iServer.Driver = _driver;
            btnSetup.Enabled = true;

            ShowPulseGuiding();
            
        }

        private void ShowPulseGuiding()
        {
            
        }

        private void SetupServer()
        {
            iServer = new ScopeServer.GuidingServer();
            iServer.Port = _port;
            iServer.Driver = _driver;

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
            _driver.Setup();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_driver != null)
            {
                _driver.Disconnect();
            }
        }

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PortDialog dialog = new PortDialog();
            dialog.Port = _port;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.Port != _port && iServer != null)
                {
                    _port = dialog.Port;
                    iServer.Start();
                }
            }
        }
    }
}
