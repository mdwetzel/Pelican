using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Data;
using Data.Packets;
using Data.Packets.Server;

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
            server.ServerOffline += new Server.ServerOfflineHandler(server_ServerOffline);
            toolStripStatusLabel3.Alignment = ToolStripItemAlignment.Right;

            if (!IsHandleCreated) {

                CreateHandle();

                server.Start();
            }
        }

        void server_ServerOffline()
        {
            Invoke(new MethodInvoker(delegate
            {
                Text = "Pelican | Offline";
                toolStripStatusLabel2.Text = "Offline";
                toolStripDropDownButton1.Image = new Bitmap(@"Images/offline.png");
                startServerToolStripMenuItem.Enabled = true;
                stopServerToolStripMenuItem.Enabled = false;
            }));
        }

        void server_ServerOnline()
        {
            Invoke(new MethodInvoker(delegate
            {
                Text = "Pelican | Online";
                toolStripStatusLabel2.Text = "Online";
                toolStripDropDownButton1.Image = new Bitmap(@"Images/online.png");
                startServerToolStripMenuItem.Enabled = false;
                stopServerToolStripMenuItem.Enabled = true;
            }));
        }

        void server_UserDisconnected(Guid guid)
        {
            Invoke(new MethodInvoker(delegate
            {
                listView1.Items.RemoveByKey(guid.ToString());
                toolStripStatusLabel3.Text = string.Format("{0} users online", server.users.Count);
            }));

        }

        private void server_UserLogin(LoginPacket packet, Socket socket)
        {
            Invoke(new MethodInvoker(delegate
            {
                var user = new ListViewItem(packet.Username) { Name = server.GetUser(socket).Guid.ToString() };
                user.SubItems.AddRange(new[] { ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() });
                listView1.Items.Add(user);
                toolStripStatusLabel3.Text = string.Format("{0} users online", server.users.Count);
            }));
        }

        private void kickUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (listView1.SelectedItems.Count > 0) {

                    User user = server.GetUser(Guid.Parse(listView1.SelectedItems[0].Name));

                    listView1.Items.RemoveByKey(user.Guid.ToString());

                    server.SendPacket(user.Socket, PacketHelper.Serialize(new KickPacket(user.Guid, "You have been kicked.")));

                    server.users.Remove(user);
                    user.Socket.Shutdown(SocketShutdown.Both);

                    toolStripStatusLabel3.Text = string.Format("{0} users online", server.users.Count);
                }
            }));
        }

        private void banUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (listView1.SelectedItems.Count > 0) {

                    User user = server.GetUser(Guid.Parse(listView1.SelectedItems[0].Name));

                    listView1.Items.RemoveByKey(user.Guid.ToString());

                    server.SendPacket(user.Socket, PacketHelper.Serialize(new BanPacket(user.Guid, "You have been banned.")));

                    server.bans.Add(new Ban { IP = (((IPEndPoint)user.Socket.RemoteEndPoint).Address) });

                    server.users.Remove(user);
                    user.Socket.Shutdown(SocketShutdown.Both);

                    toolStripStatusLabel3.Text = string.Format("{0} users online", server.users.Count);
                }
            }));
        }

        private void stopServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() => listView1.Items.Clear()));

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
