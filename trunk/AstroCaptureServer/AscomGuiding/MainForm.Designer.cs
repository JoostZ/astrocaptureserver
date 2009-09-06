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
            this.lblDriver = new System.Windows.Forms.Label();
            this.btnSelectDriver = new System.Windows.Forms.Button();
            this.txtSelectedDriver = new System.Windows.Forms.TextBox();
            this.btnSetup = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtError = new System.Windows.Forms.TextBox();
            this.iTelescopeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpPulseGuiding = new System.Windows.Forms.GroupBox();
            this.txtRaPulseRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDecPulseRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iTelescopeBindingSource)).BeginInit();
            this.grpPulseGuiding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDriver
            // 
            this.lblDriver.AutoSize = true;
            this.lblDriver.Location = new System.Drawing.Point(13, 53);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(35, 13);
            this.lblDriver.TabIndex = 0;
            this.lblDriver.Text = "Driver";
            // 
            // btnSelectDriver
            // 
            this.btnSelectDriver.Location = new System.Drawing.Point(156, 76);
            this.btnSelectDriver.Name = "btnSelectDriver";
            this.btnSelectDriver.Size = new System.Drawing.Size(55, 23);
            this.btnSelectDriver.TabIndex = 1;
            this.btnSelectDriver.Text = "Select";
            this.btnSelectDriver.UseVisualStyleBackColor = true;
            this.btnSelectDriver.Click += new System.EventHandler(this.btnSelectDriver_Click);
            // 
            // txtSelectedDriver
            // 
            this.txtSelectedDriver.Location = new System.Drawing.Point(54, 50);
            this.txtSelectedDriver.Name = "txtSelectedDriver";
            this.txtSelectedDriver.ReadOnly = true;
            this.txtSelectedDriver.Size = new System.Drawing.Size(157, 20);
            this.txtSelectedDriver.TabIndex = 2;
            // 
            // btnSetup
            // 
            this.btnSetup.Enabled = false;
            this.btnSetup.Location = new System.Drawing.Point(54, 76);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(75, 23);
            this.btnSetup.TabIndex = 6;
            this.btnSetup.Text = "Setup";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(255, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.portToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // portToolStripMenuItem
            // 
            this.portToolStripMenuItem.Name = "portToolStripMenuItem";
            this.portToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.portToolStripMenuItem.Text = "Port...";
            this.portToolStripMenuItem.Click += new System.EventHandler(this.portToolStripMenuItem_Click);
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(98, 114);
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(146, 20);
            this.txtError.TabIndex = 10;
            // 
            // iTelescopeBindingSource
            // 
            //his.iTelescopeBindingSource.DataSource = typeof(ASCOM.Interface.ITelescope);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(13, 162);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(172, 20);
            this.txtMessage.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Server Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 146);
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
            this.grpPulseGuiding.Enabled = false;
            this.grpPulseGuiding.Location = new System.Drawing.Point(13, 198);
            this.grpPulseGuiding.Name = "grpPulseGuiding";
            this.grpPulseGuiding.Size = new System.Drawing.Size(172, 79);
            this.grpPulseGuiding.TabIndex = 14;
            this.grpPulseGuiding.TabStop = false;
            this.grpPulseGuiding.Text = "Pulse Guiding";
            // 
            // txtRaPulseRate
            // 
            this.txtRaPulseRate.Location = new System.Drawing.Point(35, 17);
            this.txtRaPulseRate.Name = "txtRaPulseRate";
            this.txtRaPulseRate.Size = new System.Drawing.Size(100, 20);
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
            // txtDecPulseRate
            // 
            this.txtDecPulseRate.Location = new System.Drawing.Point(35, 43);
            this.txtDecPulseRate.Name = "txtDecPulseRate";
            this.txtDecPulseRate.Size = new System.Drawing.Size(100, 20);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 290);
            this.Controls.Add(this.grpPulseGuiding);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.txtSelectedDriver);
            this.Controls.Add(this.btnSelectDriver);
            this.Controls.Add(this.lblDriver);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ASCOM Guiding";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iTelescopeBindingSource)).EndInit();
            this.grpPulseGuiding.ResumeLayout(false);
            this.grpPulseGuiding.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.Button btnSelectDriver;
        private System.Windows.Forms.TextBox txtSelectedDriver;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.BindingSource iTelescopeBindingSource;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpPulseGuiding;
        private System.Windows.Forms.TextBox txtRaPulseRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDecPulseRate;
        private System.Windows.Forms.Label label4;
    }
}

