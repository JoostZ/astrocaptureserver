namespace AstroCaptureServer
{
    namespace AscomGuiding
    {
        partial class MainForm
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
                this.components = new System.ComponentModel.Container();
                this.btnSelectDriver = new System.Windows.Forms.Button();
                this.txtSelectedDriver = new System.Windows.Forms.TextBox();
                this.btnSetup = new System.Windows.Forms.Button();
                this.txtError = new System.Windows.Forms.TextBox();
                this.iTelescopeBindingSource = new System.Windows.Forms.BindingSource(this.components);
                this.txtMessage = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.grpPulseGuiding = new System.Windows.Forms.GroupBox();
                this.txtDecPulseRate = new System.Windows.Forms.TextBox();
                this.label4 = new System.Windows.Forms.Label();
                this.txtRaPulseRate = new System.Windows.Forms.TextBox();
                this.label3 = new System.Windows.Forms.Label();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.btnChangePort = new System.Windows.Forms.Button();
                this.txtPort = new System.Windows.Forms.TextBox();
                this.label5 = new System.Windows.Forms.Label();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                ((System.ComponentModel.ISupportInitialize)(this.iTelescopeBindingSource)).BeginInit();
                this.grpPulseGuiding.SuspendLayout();
                this.groupBox1.SuspendLayout();
                this.groupBox2.SuspendLayout();
                this.SuspendLayout();
                // 
                // btnSelectDriver
                // 
                this.btnSelectDriver.Location = new System.Drawing.Point(111, 39);
                this.btnSelectDriver.Name = "btnSelectDriver";
                this.btnSelectDriver.Size = new System.Drawing.Size(55, 23);
                this.btnSelectDriver.TabIndex = 1;
                this.btnSelectDriver.Text = "Select";
                this.btnSelectDriver.UseVisualStyleBackColor = true;
                this.btnSelectDriver.Click += new System.EventHandler(this.btnSelectDriver_Click);
                // 
                // txtSelectedDriver
                // 
                this.txtSelectedDriver.Location = new System.Drawing.Point(9, 13);
                this.txtSelectedDriver.Name = "txtSelectedDriver";
                this.txtSelectedDriver.ReadOnly = true;
                this.txtSelectedDriver.Size = new System.Drawing.Size(157, 20);
                this.txtSelectedDriver.TabIndex = 2;
                // 
                // btnSetup
                // 
                this.btnSetup.Enabled = false;
                this.btnSetup.Location = new System.Drawing.Point(9, 39);
                this.btnSetup.Name = "btnSetup";
                this.btnSetup.Size = new System.Drawing.Size(75, 23);
                this.btnSetup.TabIndex = 6;
                this.btnSetup.Text = "Setup";
                this.btnSetup.UseVisualStyleBackColor = true;
                this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
                // 
                // txtError
                // 
                this.txtError.Location = new System.Drawing.Point(44, 41);
                this.txtError.Name = "txtError";
                this.txtError.Size = new System.Drawing.Size(134, 20);
                this.txtError.TabIndex = 10;
                // 
                // txtMessage
                // 
                this.txtMessage.Location = new System.Drawing.Point(5, 83);
                this.txtMessage.Name = "txtMessage";
                this.txtMessage.Size = new System.Drawing.Size(172, 20);
                this.txtMessage.TabIndex = 11;
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(6, 44);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(37, 13);
                this.label1.TabIndex = 12;
                this.label1.Text = "Status";
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(6, 67);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(94, 13);
                this.label2.TabIndex = 13;
                this.label2.Text = "Message received";
                // 
                // grpPulseGuiding
                // 
                this.grpPulseGuiding.Controls.Add(this.txtDecPulseRate);
                this.grpPulseGuiding.Controls.Add(this.label4);
                this.grpPulseGuiding.Controls.Add(this.txtRaPulseRate);
                this.grpPulseGuiding.Controls.Add(this.label3);
                this.grpPulseGuiding.Location = new System.Drawing.Point(5, 69);
                this.grpPulseGuiding.Name = "grpPulseGuiding";
                this.grpPulseGuiding.Size = new System.Drawing.Size(172, 79);
                this.grpPulseGuiding.TabIndex = 14;
                this.grpPulseGuiding.TabStop = false;
                this.grpPulseGuiding.Text = "Pulse Guiding";
                // 
                // txtDecPulseRate
                // 
                this.txtDecPulseRate.Location = new System.Drawing.Point(35, 43);
                this.txtDecPulseRate.Name = "txtDecPulseRate";
                this.txtDecPulseRate.ReadOnly = true;
                this.txtDecPulseRate.Size = new System.Drawing.Size(60, 20);
                this.txtDecPulseRate.TabIndex = 3;
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.Location = new System.Drawing.Point(7, 46);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(29, 13);
                this.label4.TabIndex = 2;
                this.label4.Text = "DEC";
                // 
                // txtRaPulseRate
                // 
                this.txtRaPulseRate.Location = new System.Drawing.Point(35, 17);
                this.txtRaPulseRate.Name = "txtRaPulseRate";
                this.txtRaPulseRate.ReadOnly = true;
                this.txtRaPulseRate.Size = new System.Drawing.Size(60, 20);
                this.txtRaPulseRate.TabIndex = 1;
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(7, 20);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(22, 13);
                this.label3.TabIndex = 0;
                this.label3.Text = "RA";
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.btnChangePort);
                this.groupBox1.Controls.Add(this.txtPort);
                this.groupBox1.Controls.Add(this.label5);
                this.groupBox1.Controls.Add(this.txtError);
                this.groupBox1.Controls.Add(this.label2);
                this.groupBox1.Controls.Add(this.label1);
                this.groupBox1.Controls.Add(this.txtMessage);
                this.groupBox1.Location = new System.Drawing.Point(6, 12);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(201, 112);
                this.groupBox1.TabIndex = 15;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Server";
                // 
                // btnChangePort
                // 
                this.btnChangePort.Location = new System.Drawing.Point(103, 15);
                this.btnChangePort.Name = "btnChangePort";
                this.btnChangePort.Size = new System.Drawing.Size(75, 23);
                this.btnChangePort.TabIndex = 16;
                this.btnChangePort.Text = "Change";
                this.btnChangePort.UseVisualStyleBackColor = true;
                this.btnChangePort.Click += new System.EventHandler(this.btnChangePort_Click);
                // 
                // txtPort
                // 
                this.txtPort.Location = new System.Drawing.Point(44, 17);
                this.txtPort.Name = "txtPort";
                this.txtPort.ReadOnly = true;
                this.txtPort.Size = new System.Drawing.Size(45, 20);
                this.txtPort.TabIndex = 15;
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Location = new System.Drawing.Point(16, 20);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(26, 13);
                this.label5.TabIndex = 14;
                this.label5.Text = "Port";
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.grpPulseGuiding);
                this.groupBox2.Controls.Add(this.btnSetup);
                this.groupBox2.Controls.Add(this.btnSelectDriver);
                this.groupBox2.Controls.Add(this.txtSelectedDriver);
                this.groupBox2.Location = new System.Drawing.Point(6, 140);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(201, 160);
                this.groupBox2.TabIndex = 4;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Driver";
                // 
                // MainForm
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(216, 311);
                this.Controls.Add(this.groupBox1);
                this.Controls.Add(this.groupBox2);
                this.MaximizeBox = false;
                this.Name = "MainForm";
                this.Text = "ASCOM Guiding";
                this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
                ((System.ComponentModel.ISupportInitialize)(this.iTelescopeBindingSource)).EndInit();
                this.grpPulseGuiding.ResumeLayout(false);
                this.grpPulseGuiding.PerformLayout();
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                this.groupBox2.ResumeLayout(false);
                this.groupBox2.PerformLayout();
                this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.Button btnSelectDriver;
            private System.Windows.Forms.TextBox txtSelectedDriver;
            private System.Windows.Forms.Button btnSetup;
            private System.Windows.Forms.BindingSource iTelescopeBindingSource;
            private System.Windows.Forms.TextBox txtError;
            private System.Windows.Forms.TextBox txtMessage;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.GroupBox grpPulseGuiding;
            private System.Windows.Forms.TextBox txtRaPulseRate;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.TextBox txtDecPulseRate;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.GroupBox groupBox1;
            private System.Windows.Forms.Button btnChangePort;
            private System.Windows.Forms.TextBox txtPort;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.GroupBox groupBox2;
        }
    }
}