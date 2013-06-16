namespace Server
{
    sealed partial class ServerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.lstViewUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kickUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendBroadcastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnConnectionStatus = new System.Windows.Forms.ToolStripDropDownButton();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsersOnline = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.lstViewRooms = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kickRoomUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.tabRooms.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstViewUsers
            // 
            this.lstViewUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstViewUsers.ContextMenuStrip = this.contextMenuStrip1;
            this.lstViewUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewUsers.Location = new System.Drawing.Point(3, 3);
            this.lstViewUsers.Name = "lstViewUsers";
            this.lstViewUsers.Size = new System.Drawing.Size(546, 315);
            this.lstViewUsers.TabIndex = 0;
            this.lstViewUsers.UseCompatibleStateImageBehavior = false;
            this.lstViewUsers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Username";
            this.columnHeader1.Width = 149;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Room";
            this.columnHeader3.Width = 91;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kickUserToolStripMenuItem,
            this.banUserToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 48);
            // 
            // kickUserToolStripMenuItem
            // 
            this.kickUserToolStripMenuItem.Name = "kickUserToolStripMenuItem";
            this.kickUserToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.kickUserToolStripMenuItem.Text = "Kick User";
            this.kickUserToolStripMenuItem.Click += new System.EventHandler(this.kickUserToolStripMenuItem_Click);
            // 
            // banUserToolStripMenuItem
            // 
            this.banUserToolStripMenuItem.Name = "banUserToolStripMenuItem";
            this.banUserToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.banUserToolStripMenuItem.Text = "Ban User";
            this.banUserToolStripMenuItem.Click += new System.EventHandler(this.banUserToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(560, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.sendBroadcastToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // sendBroadcastToolStripMenuItem
            // 
            this.sendBroadcastToolStripMenuItem.Name = "sendBroadcastToolStripMenuItem";
            this.sendBroadcastToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.sendBroadcastToolStripMenuItem.Text = "Send Broadcast";
            this.sendBroadcastToolStripMenuItem.Click += new System.EventHandler(this.sendBroadcastToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.btnConnectionStatus,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel4,
            this.lblUsersOnline});
            this.statusStrip1.Location = new System.Drawing.Point(0, 377);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(560, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // btnConnectionStatus
            // 
            this.btnConnectionStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConnectionStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.stopServerToolStripMenuItem});
            this.btnConnectionStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnConnectionStatus.Image")));
            this.btnConnectionStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnectionStatus.Name = "btnConnectionStatus";
            this.btnConnectionStatus.Size = new System.Drawing.Size(29, 20);
            this.btnConnectionStatus.Text = "btnConnectionStatus";
            this.btnConnectionStatus.DropDownOpening += new System.EventHandler(this.btnConnectionStatus_DropDownOpening);
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.startServerToolStripMenuItem.Text = "Start Server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.stopServerToolStripMenuItem.Text = "Stop Server";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.stopServerToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel2.Text = "Offline";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            // 
            // lblUsersOnline
            // 
            this.lblUsersOnline.Name = "lblUsersOnline";
            this.lblUsersOnline.Size = new System.Drawing.Size(473, 17);
            this.lblUsersOnline.Spring = true;
            this.lblUsersOnline.Text = "0 Users Online";
            this.lblUsersOnline.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabUsers);
            this.tabMain.Controls.Add(this.tabRooms);
            this.tabMain.Location = new System.Drawing.Point(0, 27);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(560, 347);
            this.tabMain.TabIndex = 4;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.lstViewUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(552, 321);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Users";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // tabRooms
            // 
            this.tabRooms.Controls.Add(this.lstViewRooms);
            this.tabRooms.Location = new System.Drawing.Point(4, 22);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabRooms.Size = new System.Drawing.Size(552, 321);
            this.tabRooms.TabIndex = 1;
            this.tabRooms.Text = "Rooms";
            this.tabRooms.UseVisualStyleBackColor = true;
            // 
            // lstViewRooms
            // 
            this.lstViewRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader5});
            this.lstViewRooms.ContextMenuStrip = this.contextMenuStrip2;
            this.lstViewRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewRooms.Location = new System.Drawing.Point(3, 3);
            this.lstViewRooms.Name = "lstViewRooms";
            this.lstViewRooms.Size = new System.Drawing.Size(546, 315);
            this.lstViewRooms.TabIndex = 0;
            this.lstViewRooms.UseCompatibleStateImageBehavior = false;
            this.lstViewRooms.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            this.columnHeader6.Width = 87;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Users";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeRoomToolStripMenuItem,
            this.kickRoomUsersToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(172, 48);
            // 
            // closeRoomToolStripMenuItem
            // 
            this.closeRoomToolStripMenuItem.Name = "closeRoomToolStripMenuItem";
            this.closeRoomToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.closeRoomToolStripMenuItem.Text = "Close Room...";
            this.closeRoomToolStripMenuItem.Click += new System.EventHandler(this.closeRoomToolStripMenuItem_Click);
            // 
            // kickRoomUsersToolStripMenuItem
            // 
            this.kickRoomUsersToolStripMenuItem.Name = "kickRoomUsersToolStripMenuItem";
            this.kickRoomUsersToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.kickRoomUsersToolStripMenuItem.Text = "Kick Room Users...";
            this.kickRoomUsersToolStripMenuItem.Click += new System.EventHandler(this.kickRoomUsersToolStripMenuItem_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 399);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ServerForm";
            this.Text = "Pelican | Offline";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabRooms.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstViewUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kickUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banUserToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton btnConnectionStatus;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblUsersOnline;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendBroadcastToolStripMenuItem;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabRooms;
        private System.Windows.Forms.ListView lstViewRooms;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem closeRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickRoomUsersToolStripMenuItem;

    }
}

