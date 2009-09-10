using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ASCOM.DriverAccess;

using AstroCaptureServer.Server;
using AstroCaptureServer.Driver;

namespace AstroCaptureServer
{
    namespace AscomGuiding
    {
        /// <summary>
        /// AstroCapture Guiding application
        /// </summary>
        public partial class MainForm : Form
        {
            private AscomDriver iDriver = null;
            private AstroCaptureServer.Server.GuidingServer iServer = null;

            /// <summary>
            /// Constructor
            /// </summary>
            public MainForm()
            {
                InitializeComponent();
                SetupServer();
                txtPort.Text = iServer.Port.ToString();
            }

            /// <summary>
            /// Select an ASCOM driver
            /// </summary>
            /// <param name="sender">sender of the event</param>
            /// <param name="e">arguments associated with the event</param>
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

            /// <summary>
            /// Display data on PulseGuiding
            /// </summary>
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

            /// <summary>
            /// Initialize the Guiding sever
            /// </summary>
            private void SetupServer()
            {
                iServer = new GuidingServer();
                iServer.Driver = iDriver;

                iServer.OnStatusMessage += new GuidingServer.StatusMessageHandler(DoError);
                iServer.OnMessageReceived += new GuidingServer.StatusMessageHandler(ShowMessage);
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
                dialog.Port = iServer.Port;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.Port != iServer.Port)
                    {
                        iServer.Port = dialog.Port;
                        txtPort.Text = iServer.Port.ToString();
                        iServer.ReStart();
                    }
                }

            }
        }
    }
}