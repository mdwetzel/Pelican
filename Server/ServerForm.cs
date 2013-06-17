﻿#region Using
using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Data;
using Data.Packets.Client;
using Server.Properties;
#endregion

namespace Server
{
    public sealed partial class ServerForm : Form
    {
        #region Fields/Properties
        private readonly Server server;
        #endregion

        #region Constructors
        public ServerForm()
        {
            InitializeComponent();

            server = new Server();

            server.UserLogin += server_UserLogin;
            server.UserDisconnected += server_UserDisconnected;
            server.ServerOnline += server_ServerOnline;
            server.ServerOffline += server_ServerOffline;
            server.UserJoinRoom += server_UserJoinRoom;
            server.RoomCreated += server_RoomCreated;
            lblUsersOnline.Alignment = ToolStripItemAlignment.Right;

            if (!IsHandleCreated) {
                CreateHandle();
                server.Start();
                server.LoadRooms();
            }
        }
        #endregion

        #region Methods
        private void ToggleConnectionButtons(bool connected)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (connected) {
                    startServerToolStripMenuItem.Enabled = true;
                    stopServerToolStripMenuItem.Enabled = false;
                } else {
                    startServerToolStripMenuItem.Enabled = false;
                    stopServerToolStripMenuItem.Enabled = true;
                }
            }));
        }

        private void UpdateUsersOnline(int count)
        {
            Invoke(new MethodInvoker(delegate
            {
                lblUsersOnline.Text = string.Format("{0} users online", count);
            }));
        }
        #endregion

        #region Server Event Handlers
        private void server_RoomCreated(Room room)
        {
            Invoke(new MethodInvoker(delegate
            {
                var item = new ListViewItem(room.Name) { Name = room.Guid.ToString() };
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, room.Description) { Name = @"Description" });
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, room.Users.Count.ToString(CultureInfo.InvariantCulture)) { Name = @"Count" });
                lstViewRooms.Items.Add(item);
            }));
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.SaveRooms();
        }

        private void server_UserJoinRoom(JoinRoomPacket packet, Socket socket)
        {
            User user = server.GetUser(socket);
            Room room = server.GetRoom(packet.Guid);

            Invoke(new MethodInvoker(() => lstViewUsers.Items[user.Guid.ToString()].SubItems["Room"].Text = room.Name));
        }

        private void server_ServerOffline()
        {
            Invoke(new MethodInvoker(delegate
            {
                Text = string.Format("{0} | {1}", Resources.BaseName, Resources.Offline);
                toolStripStatusLabel2.Text = Resources.Offline;
                btnConnectionStatus.Image = new Bitmap(@"Images/offline.png");

                ToggleConnectionButtons(false);
            }));
        }

        private void server_ServerOnline()
        {
            Invoke(new MethodInvoker(delegate
            {
                Text = string.Format("{0} | {1} - Listening on Port {2}", Resources.BaseName, Resources.Online, Configuration.Port);
                toolStripStatusLabel2.Text = Resources.Online;
                btnConnectionStatus.Image = new Bitmap(@"Images/online.png");

                ToggleConnectionButtons(true);
            }));
        }

        private void server_UserDisconnected(Guid guid)
        {
            Invoke(new MethodInvoker(delegate
            {
                lstViewUsers.Items.RemoveByKey(guid.ToString());
                UpdateUsersOnline(server.Users.Count);
            }));
        }

        private void server_UserLogin(LoginPacket packet, Socket socket)
        {
            Invoke(new MethodInvoker(delegate
            {
                var user = new ListViewItem(packet.Username) { Name = server.GetUser(socket).Guid.ToString() };
                user.SubItems.Add(new ListViewItem.ListViewSubItem(user, ((IPEndPoint)socket.RemoteEndPoint).Address.ToString()) { Name = @"IP" });
                user.SubItems.Add(new ListViewItem.ListViewSubItem(user, "Roomless") { Name = @"Room" });

                lstViewUsers.Items.Add(user);
                UpdateUsersOnline(server.Users.Count);
            }));
        }
        #endregion

        #region Form Event Handlers

        private void kickRoomUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CustomMessage customMessage = new CustomMessage()) {
                if (customMessage.ShowDialog() == DialogResult.OK) {
                    if (lstViewRooms.SelectedItems.Count > 0) {
                        server.KickUsers(Guid.Parse(lstViewRooms.SelectedItems[0].Name), customMessage.Message);
                    }
                }
            }
        }

        private void closeRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CustomMessage customMessage = new CustomMessage()) {
                if (customMessage.ShowDialog() == DialogResult.OK) {
                    if (lstViewRooms.SelectedItems.Count > 0) {
                        server.CloseRoom(Guid.Parse(lstViewRooms.SelectedItems[0].Name), customMessage.Message);
                        lstViewRooms.SelectedItems[0].Remove();
                    }
                }
            }
        }

        private void kickUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (lstViewUsers.SelectedItems.Count <= 0) return;

                User user = server.GetUser(Guid.Parse(lstViewUsers.SelectedItems[0].Name));

                lstViewUsers.Items.RemoveByKey(user.Guid.ToString());

                server.KickUser(user);

                UpdateUsersOnline(server.Users.Count);
            }));
        }

        private void banUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (lstViewUsers.SelectedItems.Count <= 0) return;

                User user = server.GetUser(Guid.Parse(lstViewUsers.SelectedItems[0].Name));

                lstViewUsers.Items.RemoveByKey(user.Guid.ToString());

                server.BanUser(user);

                UpdateUsersOnline(server.Users.Count);
            }));
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() => lstViewUsers.Items.Clear()));

            server.Stop();

            UpdateUsersOnline(server.Users.Count);
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Start();
        }

        private void btnConnectionStatus_DropDownOpening(object sender, EventArgs e)
        {
            ToggleConnectionButtons(!server.Online);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Stop();
            Application.Exit();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigurationForm configurationForm = new ConfigurationForm()) {
                if (configurationForm.ShowDialog() == DialogResult.OK) {
                    Configuration.IpAddress = configurationForm.IpAddress;
                    Configuration.Port = configurationForm.Port;

                    Settings.Default.IpAddress = configurationForm.IpAddress.ToString();
                    Settings.Default.Port = configurationForm.Port.ToString(CultureInfo.InvariantCulture);

                    Settings.Default.Save();
                }
            }
        }

        private void sendBroadcastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BroadcastForm broadcastForm = new BroadcastForm()) {
                if (broadcastForm.ShowDialog() == DialogResult.OK) {
                    server.SendBroadcast(broadcastForm.Message);
                }
            }
        }
        #endregion
    }
}
