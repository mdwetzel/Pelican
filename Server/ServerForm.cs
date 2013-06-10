using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Data;
using Data.Packets;
using Data.Packets.Client;
using Data.Packets.Server;
using Server.Properties;

namespace Server
{
    public sealed partial class ServerForm : Form
    {
        private readonly Server server;

        public ServerForm()
        {
            InitializeComponent();

            server = new Server();

            server.UserLogin += server_UserLogin;
            server.UserDisconnected += server_UserDisconnected;
            server.ServerOnline += server_ServerOnline;
            server.ServerOffline += server_ServerOffline;
            lblUsersOnline.Alignment = ToolStripItemAlignment.Right;
            server.UserJoinRoom += server_UserJoinRoom;

            if (!IsHandleCreated) {

                CreateHandle();

                server.Start();
            }
        }

        void server_UserJoinRoom(Data.Packets.Client.JoinRoomPacket packet, Socket socket)
        {
            User user = server.GetUser(socket);
            Room room = server.GetRoom(packet.Guid);

            Invoke(new MethodInvoker(() => lstViewUsers.Items[user.Guid.ToString()].SubItems[2].Text = room.Name));
        }

        void server_ServerOffline()
        {
            Invoke(new MethodInvoker(delegate
            {
                Text = string.Format("{0} | {1}", Resources.BaseName, Resources.Offline);
                toolStripStatusLabel2.Text = Resources.Offline;
                toolStripDropDownButton1.Image = new Bitmap(@"Images/offline.png");
                startServerToolStripMenuItem.Enabled = true;
                stopServerToolStripMenuItem.Enabled = false;
            }));
        }

        void server_ServerOnline()
        {
            Invoke(new MethodInvoker(delegate
            {
                Text = string.Format("{0} | {1}", Resources.BaseName, Resources.Online);
                toolStripStatusLabel2.Text = Resources.Online;
                toolStripDropDownButton1.Image = new Bitmap(@"Images/online.png");
                startServerToolStripMenuItem.Enabled = false;
                stopServerToolStripMenuItem.Enabled = true;
            }));
        }

        void server_UserDisconnected(Guid guid)
        {
            Invoke(new MethodInvoker(delegate
            {
                lstViewUsers.Items.RemoveByKey(guid.ToString());
                lblUsersOnline.Text = string.Format("{0} users online", server.users.Count);
            }));

        }

        private void server_UserLogin(LoginPacket packet, Socket socket)
        {
            Invoke(new MethodInvoker(delegate
            {
                var user = new ListViewItem(packet.Username) { Name = server.GetUser(socket).Guid.ToString() };
                user.SubItems.AddRange(new[] { ((IPEndPoint)socket.RemoteEndPoint).Address.ToString(), "Roomless" });
                lstViewUsers.Items.Add(user);
                lblUsersOnline.Text = string.Format("{0} users online", server.users.Count);
            }));
        }

        private void kickUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (lstViewUsers.SelectedItems.Count > 0) {

                    User user = server.GetUser(Guid.Parse(lstViewUsers.SelectedItems[0].Name));

                    lstViewUsers.Items.RemoveByKey(user.Guid.ToString());

                    if (user.Room == null) {
                        server.SendPacket(user.Socket,
                                          PacketHelper.Serialize(new KickPacket(user.Guid, "You have been kicked.",
                                                                                string.Format(
                                                                                    "<<< {0} has been kicked >>>",
                                                                                    user.Username))));
                    } else {
                        server.SendPacket(user.Room,
                  PacketHelper.Serialize(new KickPacket(user.Guid, "You have been kicked.",
                                                        string.Format(
                                                            "<<< {0} has been kicked from {1} >>>",
                                                            user.Username, user.Room.Name))));
                    }

                    server.users.Remove(user);
                    user.Socket.Shutdown(SocketShutdown.Both);

                    if (user.Room != null) {
                        user.Room.Users.Remove(user);

                        server.SendPacket(user.Room, PacketHelper.Serialize(new RefreshUsersPacket(user.Room.Users)));
                    }

                    lblUsersOnline.Text = string.Format("{0} users online", server.users.Count);
                }
            }));
        }

        private void banUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (lstViewUsers.SelectedItems.Count > 0) {

                    User user = server.GetUser(Guid.Parse(lstViewUsers.SelectedItems[0].Name));

                    lstViewUsers.Items.RemoveByKey(user.Guid.ToString());

                    server.SendPacket(user.Socket, PacketHelper.Serialize(new BanPacket(user.Guid, "You have been banned.")));

                    server.bans.Add(new Ban { IP = (((IPEndPoint)user.Socket.RemoteEndPoint).Address) });

                    server.users.Remove(user);
                    user.Socket.Shutdown(SocketShutdown.Both);

                    user.Room.Users.Remove(user);

                    lblUsersOnline.Text = string.Format("{0} users online", server.users.Count);
                }
            }));
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() => lstViewUsers.Items.Clear()));

            server.Stop();
        }

        private void startServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            server.Start();
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
