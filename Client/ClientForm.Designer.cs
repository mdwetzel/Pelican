﻿namespace Client
{
    partial class ClientForm
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
            this.rchTxtChat = new System.Windows.Forms.RichTextBox();
            this.lstViewUsers = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageRooms = new System.Windows.Forms.TabPage();
            this.rchTxtLog = new System.Windows.Forms.RichTextBox();
            this.lstViewRooms = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageRoom = new System.Windows.Forms.TabPage();
            this.btnSend = new System.Windows.Forms.Button();
            this.rchTxtInput = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPageRooms.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPageRoom.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rchTxtChat
            // 
            this.rchTxtChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rchTxtChat.Location = new System.Drawing.Point(0, 3);
            this.rchTxtChat.Name = "rchTxtChat";
            this.rchTxtChat.Size = new System.Drawing.Size(748, 468);
            this.rchTxtChat.TabIndex = 0;
            this.rchTxtChat.Text = "";
            // 
            // lstViewUsers
            // 
            this.lstViewUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstViewUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.lstViewUsers.Location = new System.Drawing.Point(754, 0);
            this.lstViewUsers.Name = "lstViewUsers";
            this.lstViewUsers.Size = new System.Drawing.Size(142, 471);
            this.lstViewUsers.TabIndex = 1;
            this.lstViewUsers.UseCompatibleStateImageBehavior = false;
            this.lstViewUsers.View = System.Windows.Forms.View.Details;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageRooms);
            this.tabControl1.Controls.Add(this.tabPageRoom);
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(904, 531);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageRooms
            // 
            this.tabPageRooms.Controls.Add(this.rchTxtLog);
            this.tabPageRooms.Controls.Add(this.lstViewRooms);
            this.tabPageRooms.Location = new System.Drawing.Point(4, 22);
            this.tabPageRooms.Name = "tabPageRooms";
            this.tabPageRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRooms.Size = new System.Drawing.Size(896, 505);
            this.tabPageRooms.TabIndex = 0;
            this.tabPageRooms.Text = "Chat Rooms";
            this.tabPageRooms.UseVisualStyleBackColor = true;
            // 
            // rchTxtLog
            // 
            this.rchTxtLog.Location = new System.Drawing.Point(0, 409);
            this.rchTxtLog.Name = "rchTxtLog";
            this.rchTxtLog.ReadOnly = true;
            this.rchTxtLog.Size = new System.Drawing.Size(896, 96);
            this.rchTxtLog.TabIndex = 6;
            this.rchTxtLog.Text = "";
            // 
            // lstViewRooms
            // 
            this.lstViewRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lstViewRooms.ContextMenuStrip = this.contextMenuStrip1;
            this.lstViewRooms.Location = new System.Drawing.Point(3, 3);
            this.lstViewRooms.Name = "lstViewRooms";
            this.lstViewRooms.Size = new System.Drawing.Size(890, 400);
            this.lstViewRooms.TabIndex = 0;
            this.lstViewRooms.UseCompatibleStateImageBehavior = false;
            this.lstViewRooms.View = System.Windows.Forms.View.Details;
            this.lstViewRooms.DoubleClick += new System.EventHandler(this.lstViewRooms_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 244;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 508;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Users";
            this.columnHeader3.Width = 61;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Private";
            this.columnHeader4.Width = 69;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createRoomToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 26);
            // 
            // createRoomToolStripMenuItem
            // 
            this.createRoomToolStripMenuItem.Name = "createRoomToolStripMenuItem";
            this.createRoomToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.createRoomToolStripMenuItem.Text = "Create Room";
            this.createRoomToolStripMenuItem.Click += new System.EventHandler(this.createRoomToolStripMenuItem_Click);
            // 
            // tabPageRoom
            // 
            this.tabPageRoom.Controls.Add(this.btnSend);
            this.tabPageRoom.Controls.Add(this.rchTxtInput);
            this.tabPageRoom.Controls.Add(this.rchTxtChat);
            this.tabPageRoom.Controls.Add(this.lstViewUsers);
            this.tabPageRoom.Location = new System.Drawing.Point(4, 22);
            this.tabPageRoom.Name = "tabPageRoom";
            this.tabPageRoom.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRoom.Size = new System.Drawing.Size(896, 505);
            this.tabPageRoom.TabIndex = 1;
            this.tabPageRoom.Text = "Room";
            this.tabPageRoom.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(754, 477);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(139, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rchTxtInput
            // 
            this.rchTxtInput.Location = new System.Drawing.Point(3, 476);
            this.rchTxtInput.Multiline = false;
            this.rchTxtInput.Name = "rchTxtInput";
            this.rchTxtInput.Size = new System.Drawing.Size(745, 24);
            this.rchTxtInput.TabIndex = 4;
            this.rchTxtInput.Text = "";
            this.rchTxtInput.TextChanged += new System.EventHandler(this.rchTxtInput_TextChanged);
            this.rchTxtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rchTxtInput_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(904, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Enabled = false;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Username";
            this.columnHeader5.Width = 79;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 558);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClientForm";
            this.Text = "Client";
            this.tabControl1.ResumeLayout(false);
            this.tabPageRooms.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPageRoom.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rchTxtChat;
        private System.Windows.Forms.ListView lstViewUsers;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageRooms;
        private System.Windows.Forms.TabPage tabPageRoom;
        private System.Windows.Forms.ListView lstViewRooms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createRoomToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rchTxtLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox rchTxtInput;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

