namespace Client
{
    partial class OptionsForm
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
            if (disposing && (components != null)) {
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
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.grpBoxServer = new System.Windows.Forms.GroupBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBoxUser = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.grpBoxServer.SuspendLayout();
            this.grpBoxUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(78, 19);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(154, 20);
            this.txtIpAddress.TabIndex = 1;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(78, 45);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(154, 20);
            this.txtPort.TabIndex = 2;
            // 
            // grpBoxServer
            // 
            this.grpBoxServer.Controls.Add(this.lblPort);
            this.grpBoxServer.Controls.Add(this.lblIpAddress);
            this.grpBoxServer.Controls.Add(this.txtIpAddress);
            this.grpBoxServer.Controls.Add(this.txtPort);
            this.grpBoxServer.Location = new System.Drawing.Point(12, 80);
            this.grpBoxServer.Name = "grpBoxServer";
            this.grpBoxServer.Size = new System.Drawing.Size(238, 75);
            this.grpBoxServer.TabIndex = 2;
            this.grpBoxServer.TabStop = false;
            this.grpBoxServer.Text = "Server";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 52);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Port";
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Location = new System.Drawing.Point(6, 26);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(58, 13);
            this.lblIpAddress.TabIndex = 2;
            this.lblIpAddress.Text = "IP Address";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(90, 161);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(175, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpBoxUser
            // 
            this.grpBoxUser.Controls.Add(this.txtUsername);
            this.grpBoxUser.Controls.Add(this.lblUsername);
            this.grpBoxUser.Location = new System.Drawing.Point(12, 12);
            this.grpBoxUser.Name = "grpBoxUser";
            this.grpBoxUser.Size = new System.Drawing.Size(238, 62);
            this.grpBoxUser.TabIndex = 5;
            this.grpBoxUser.TabStop = false;
            this.grpBoxUser.Text = "User";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(78, 20);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(154, 20);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Focus();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 27);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 196);
            this.Controls.Add(this.grpBoxUser);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpBoxServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.grpBoxServer.ResumeLayout(false);
            this.grpBoxServer.PerformLayout();
            this.grpBoxUser.ResumeLayout(false);
            this.grpBoxUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.GroupBox grpBoxServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpBoxUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;

    }
}