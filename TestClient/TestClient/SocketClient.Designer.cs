namespace SocketClient
{
    partial class SocketClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDE = new System.Windows.Forms.Label();
            this.lblRA = new System.Windows.Forms.Label();
            this.txtDETime = new System.Windows.Forms.TextBox();
            this.txtRATime = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSending = new System.Windows.Forms.TextBox();
            this.btnChangePort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(54, 16);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(41, 20);
            this.txtPort.TabIndex = 0;
            this.txtPort.Text = "4242";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // lblDE
            // 
            this.lblDE.AutoSize = true;
            this.lblDE.Location = new System.Drawing.Point(26, 46);
            this.lblDE.Name = "lblDE";
            this.lblDE.Size = new System.Drawing.Size(22, 13);
            this.lblDE.TabIndex = 2;
            this.lblDE.Text = "DE";
            // 
            // lblRA
            // 
            this.lblRA.AutoSize = true;
            this.lblRA.Location = new System.Drawing.Point(26, 69);
            this.lblRA.Name = "lblRA";
            this.lblRA.Size = new System.Drawing.Size(22, 13);
            this.lblRA.TabIndex = 3;
            this.lblRA.Text = "RA";
            // 
            // txtDETime
            // 
            this.txtDETime.Location = new System.Drawing.Point(54, 43);
            this.txtDETime.Name = "txtDETime";
            this.txtDETime.Size = new System.Drawing.Size(100, 20);
            this.txtDETime.TabIndex = 4;
            // 
            // txtRATime
            // 
            this.txtRATime.Location = new System.Drawing.Point(54, 66);
            this.txtRATime.Name = "txtRATime";
            this.txtRATime.Size = new System.Drawing.Size(100, 20);
            this.txtRATime.TabIndex = 5;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(183, 41);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnConnect
            // 
            this.btnDisConnect.Enabled = false;
            this.btnDisConnect.Location = new System.Drawing.Point(183, 14);
            this.btnDisConnect.Name = "btnConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisConnect.TabIndex = 7;
            this.btnDisConnect.Text = "Disconnect";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Sending";
            // 
            // txtSending
            // 
            this.txtSending.Location = new System.Drawing.Point(54, 91);
            this.txtSending.Name = "txtSending";
            this.txtSending.Size = new System.Drawing.Size(201, 20);
            this.txtSending.TabIndex = 9;
            // 
            // btnChangePort
            // 
            this.btnChangePort.Location = new System.Drawing.Point(101, 14);
            this.btnChangePort.Name = "btnChangePort";
            this.btnChangePort.Size = new System.Drawing.Size(75, 23);
            this.btnChangePort.TabIndex = 10;
            this.btnChangePort.Text = "Change Port";
            this.btnChangePort.UseVisualStyleBackColor = true;
            this.btnChangePort.Click += new System.EventHandler(this.btnChangePort_Click);
            // 
            // SocketClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 132);
            this.Controls.Add(this.btnChangePort);
            this.Controls.Add(this.txtSending);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDisConnect);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtRATime);
            this.Controls.Add(this.txtDETime);
            this.Controls.Add(this.lblRA);
            this.Controls.Add(this.lblDE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Name = "SocketClient";
            this.Text = "Socket Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDE;
        private System.Windows.Forms.Label lblRA;
        private System.Windows.Forms.TextBox txtDETime;
        private System.Windows.Forms.TextBox txtRATime;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSending;
        private System.Windows.Forms.Button btnChangePort;
    }
}

