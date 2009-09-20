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

            public Int16 UpdatePeriod { get; set; }

            public AstroCaptureServer.Server.GuidingServer Server
            {
                get
                {
                    return iServer;
                }
                set
                {
                    iServer = value;
                }
            }
            

            /// <summary>
            /// Constructor
            /// </summary>
            public MainForm()
            {
                InitializeComponent();
                mainFormBindingSource.DataSource = this;
                SetupServer();
                txtPort.Text = Server.Port.ToString();
            }

            /// <summary>
            /// Select an ASCOM driver
            /// </summary>
            /// <param name="sender">sender of the event</param>
            /// <param name="e">arguments associated with the event</param>
            private void btnSelectDriver_Click(object sender, EventArgs e)
            {
                try
                {
                    string ProgId = Telescope.Choose("");
                    if (ProgId != "")
                    {
                        if (iDriver != null)
                        {
                            iDriver.Dispose();
                        }

                        iDriver = new AscomDriver(ProgId);
                        iServer.Driver = iDriver;
                        btnSetup.Enabled = true;

                        txtSelectedDriver.Text = iDriver.Name;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

                Server.UpdatePeriod = 2000;
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

            private void pictureBox1_Click(object sender, EventArgs e)
            {
                try
                {
                    System.Diagnostics.Process.Start("http://ascom-standards.org/");
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
            }

            private void btnAbout_Click(object sender, EventArgs e)
            {
                AboutBox dialog = new AboutBox();
                dialog.ShowDialog();
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        }
    }
}