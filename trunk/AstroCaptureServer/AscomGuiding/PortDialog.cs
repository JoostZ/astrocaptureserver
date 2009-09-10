using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AstroCaptureServer
{
    namespace AscomGuiding
    {
        public partial class PortDialog : Form
        {

            public PortDialog()
            {
                InitializeComponent();
            }

            public int Port
            {
                get
                {
                    return Convert.ToInt32(txtPort.Text);
                }
                set
                {
                    txtPort.Text = value.ToString();
                }
            }
        }
    }
}