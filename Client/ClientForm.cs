using System;
using System.Windows.Forms;
using Data;
using Data.Packets;
using Data.Packets.Client;
using Data.Packets.Server;
using JoinRoomPacket = Data.Packets.Client.JoinRoomPacket;

namespace Client
{
    public partial class ClientForm : Form
    {
        private readonly Client client;

        public ClientForm()
        {
            InitializeComponent();

            client = new Client();

            client.UpdateRooms += client_UpdateRooms;
            client.ConnectionLost += client_ConnectionLost;
            client.KickUser += client_KickUser;
            client.BanUser += client_BanUser;
            client.JoinRoom += client_JoinRoom;
            client.RoomMessage += client_RoomMessage;
            client.RefreshUsers += client_RefreshUsers;
            client.UserJoinedRoom += client_UserJoinedRoom;
            client.UserLeftRoom += client_UserLeftRoom;

            client.ConnectionEstablished += client_ConnectionEstablished;
            client.BanNotification += client_BanNotification;

        }

        void client_UserLeftRoom(UserLeftRoomPacket packet)
        {
            WriteToChatWindow(packet.Message);
        }

        void client_UserJoinedRoom(UserJoinedRoomPacket packet)
        {
            WriteToChatWindow(packet.Message);
        }

        void client_RefreshUsers(RefreshUsersPacket packet)
        {
            Invoke(new MethodInvoker(delegate
            {
                lstViewUsers.Items.Clear();

                foreach (var user in packet.Users) {
                    lstViewUsers.Items.Add(user.Username);
                }
            }));
        }

        void client_RoomMessage(RoomMessagePacket packet)
        {
            WriteToChatWindow(packet.Message);
        }

        void WriteToChatWindow(string message)
        {
            Invoke(new MethodInvoker(delegate
            {
                rchTxtChat.Text += string.Format("[{0}] {1}", DateTime.Now.ToString("T"), message);
                rchTxtChat.Text += "\r\n";
                rchTxtChat.Select(rchTxtChat.Text.Length, 1);
                rchTxtChat.ScrollToCaret();
            }));
        }

        void client_JoinRoom(Data.Packets.Server.JoinRoomPacket packet)
        {
            client.room = packet.Room;

            SwitchToTab(tabPageRoom);

            Invoke(new MethodInvoker(delegate
            {
                foreach (var user in packet.Room.Users) {
                    lstViewUsers.Items.Add(user.Username);
                }
            }));
        }

        void SwitchToTab(TabPage tab)
        {
            Invoke(new MethodInvoker(delegate
                {
                    tabControl1.SelectedTab = tab;

                    if (tab == tabPageRoom) {
                        rchTxtInput.Select();
                    }

                }));
        }

        void client_BanNotification(BanNotificationPacket packet)
        {
            WriteToLog(packet.Message);
        }

        private void WriteToLog(string message)
        {
            Invoke(new MethodInvoker(delegate
            {

                rchTxtLog.Text += string.Format("[{0}] {1}", DateTime.Now.ToString("T"), message);
                rchTxtLog.Text += "\r\n";
                rchTxtLog.Select(rchTxtLog.Text.Length, 1);
                rchTxtLog.ScrollToCaret();
            }));
        }

        void client_BanUser(BanPacket packet)
        {
            if (packet.Guid == client.User.Guid) {
                WriteToLog(packet.Message);
            }
        }

        void client_KickUser(KickPacket packet)
        {
            if (packet.UserGuid == client.User.Guid) {
                WriteToLog(packet.TargetMessage);
            } else {
                WriteToChatWindow(packet.RoomMessage);
            }
        }

        void client_ConnectionEstablished(StateObject state)
        {
            Invoke(new MethodInvoker(delegate
            {
                tabPageRoom.Enabled = true;
                tabPageRooms.Enabled = true;

                Text = "Pelican";
                SwitchToTab(tabPageRooms);

                WriteToLog("Connected to server!");


            }));
        }

        private void client_ConnectionLost()
        {
            Invoke(new MethodInvoker(delegate
            {
                tabPageRoom.Enabled = false;
                tabPageRooms.Enabled = false;

                Text = "Pelican - Disconnected";
                SwitchToTab(tabPageRooms);

                client.User.Socket = null;

                WriteToLog("Lost connection to server.");
            }));
        }

        void client_UpdateRooms(UpdateRoomsPacket packet)
        {
            Invoke(new MethodInvoker(delegate
            {
                lstViewRooms.Items.Clear();
                foreach (var room in packet.Rooms) {
                    var roomItem = new ListViewItem(room.Name) { Name = room.Guid.ToString() };
                    roomItem.SubItems.AddRange(new[] { room.Description, "0", room.Private.ToString() });
                    lstViewRooms.Items.Add(roomItem);
                }
            }));
        }


        private void createRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CreateRoom createRoom = new CreateRoom()) {
                if (createRoom.ShowDialog() == DialogResult.OK) {
                    CreateRoomPacket packet = new CreateRoomPacket(createRoom.Name, createRoom.Description);

                    client.SendPacket(PacketHelper.Serialize(packet));
                }
            }
        }

        private void lstViewRooms_DoubleClick(object sender, EventArgs e)
        {
            client.SendPacket(PacketHelper.Serialize(new JoinRoomPacket(Guid.Parse(lstViewRooms.SelectedItems[0].Name))));
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Connect();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Disconnect();

            Invoke(new MethodInvoker(delegate
            {
                tabPageRoom.Enabled = false;
                tabPageRooms.Enabled = false;

                Text = "Pelican - Disconnected";
                SwitchToTab(tabPageRooms);

                WriteToLog("Lost connection to server.");
            }));
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (client.User.Socket != null && client.User.Socket.Connected) {
                disconnectToolStripMenuItem.Enabled = true;
                connectToolStripMenuItem.Enabled = false;
            }

            if (client.User.Socket == null || client.User.Socket.Connected) return;
            disconnectToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {


            client.SendPacket(PacketHelper.Serialize(new MessagePacket(rchTxtInput.Text, client.room)));

            rchTxtInput.Clear();
        }

        private void rchTxtInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void rchTxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            btnSend_Click(this, null);
            e.Handled = true;
        }
    }
}
