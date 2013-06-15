#region Using
using System;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using Client.Properties;
using Data;
using Data.Packets;
using Data.Packets.Server;
#endregion

namespace Client
{
    public partial class ClientForm : Form
    {
        #region Fields/Properties
        private readonly Client client;
        #endregion

        #region Constructors
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
            client.Broadcast += client_Broadcast;
        }

        #endregion

        #region Methods
        private void ClearListViews()
        {
            lstViewRooms.Items.Clear();
            lstViewUsers.Items.Clear();
        }

        private void ClearRoomChat()
        {
            Invoke(new MethodInvoker(() => rchTxtChat.Clear()));
        }

        private void ToggleRoomsTab(bool enabled)
        {
            Invoke(new MethodInvoker(() => rchTxtChat.Enabled = rchTxtInput.Enabled = btnSend.Enabled = lstViewUsers.Enabled = enabled));
        }

        private void WriteToChatWindow(string message)
        {
            Invoke(
                new MethodInvoker(
                    () => WriteToChatBox(rchTxtChat, string.Format("[{0}] {1}", DateTime.Now.ToString("T"), message))));
        }

        private void WriteToLog(string message)
        {
            Invoke(
                new MethodInvoker(
                    () => WriteToChatBox(rchTxtLog, string.Format("[{0}] {1}", DateTime.Now.ToString("T"), message))));
        }

        private void WriteToChatBox(TextBoxBase box, string message)
        {
            Invoke(new MethodInvoker(delegate
                {
                    box.Text += message;
                    box.Text += Resources.NewLine;
                    ScrollTextBox(box);
                }));
        }

        private void ScrollTextBox(TextBoxBase box)
        {
            Invoke(new MethodInvoker(delegate
                {
                    box.Select(box.Text.Length, 1);
                    box.ScrollToCaret();
                }));
        }

        private void SwitchToTab(TabPage tab)
        {
            Invoke(new MethodInvoker(delegate
                {
                    tabMain.SelectedTab = tab;

                    if (tab == tabPageRoom) {
                        rchTxtInput.Select();
                    }
                }));
        }

        private void ToggleTabPages(bool enabled)
        {
            Invoke(new MethodInvoker(() => tabPageRoom.Enabled = tabPageRooms.Enabled = enabled));
        }

        private void ToggleConnectButtons(bool connected)
        {
            if (connected) {
                disconnectToolStripMenuItem.Enabled = true;
                connectToolStripMenuItem.Enabled = false;
            } else {
                disconnectToolStripMenuItem.Enabled = false;
                connectToolStripMenuItem.Enabled = true;
            }
        }
        #endregion

        #region Client Event Handlers
        void client_Broadcast(BroadcastPacket packet)
        {
            Invoke(new MethodInvoker(() => MessageBox.Show(packet.Message, "Server Broadcast", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)));
        }

        private void client_RoomMessage(RoomMessagePacket packet)
        {
            WriteToChatWindow(packet.Message);
        }

        private void client_JoinRoom(JoinRoomPacket packet)
        {
            ToggleRoomsTab(true);
            SwitchToTab(tabPageRoom);

            Invoke(new MethodInvoker(delegate
            {
                foreach (var user in packet.Room.Users) {
                    lstViewUsers.Items.Add(user.Username);
                }
            }));
        }

        private void client_BanNotification(BanNotificationPacket packet)
        {
            WriteToLog(packet.Message);
        }

        private void client_UserLeftRoom(UserLeftRoomPacket packet)
        {
            WriteToChatWindow(packet.Message);
        }

        private void client_UserJoinedRoom(UserJoinedRoomPacket packet)
        {
            WriteToChatWindow(packet.Message);
        }

        private void client_RefreshUsers(RefreshUsersPacket packet)
        {
            Invoke(new MethodInvoker(delegate
                {
                    lstViewUsers.Items.Clear();

                    foreach (var user in packet.Users) {
                        lstViewUsers.Items.Add(user.Username);
                    }
                }));
        }

        private void client_BanUser(BanPacket packet)
        {
            if (packet.Guid == client.User.Guid) {
                WriteToLog(packet.Message);
            }
        }

        private void client_KickUser(KickPacket packet)
        {
            if (packet.UserGuid == client.User.Guid) {
                WriteToLog(packet.TargetMessage);
            } else {
                WriteToChatWindow(packet.RoomMessage);
            }
        }

        private void client_ConnectionEstablished(StateObject state)
        {
            Invoke(new MethodInvoker(delegate
                {
                    ToggleTabPages(true);

                    Text = Resources.BaseName;
                    SwitchToTab(tabPageRooms);

                    WriteToLog(Resources.ConnectionAchieved);
                }));
        }

        private void client_ConnectionLost()
        {
            Invoke(new MethodInvoker(delegate
                {
                    ToggleTabPages(false);

                    Text = string.Format("{0} | {1}", Resources.BaseName, Resources.Disconnected);
                    SwitchToTab(tabPageRooms);

                    client.User.Socket = null;

                    ClearListViews();

                    ClearRoomChat();

                    ToggleRoomsTab(false);

                    WriteToLog(Resources.ConnectionLost);
                }));
        }

        private void client_UpdateRooms(UpdateRoomsPacket packet)
        {
            Invoke(new MethodInvoker(delegate
                {
                    lstViewRooms.Items.Clear();
                    foreach (var room in packet.Rooms) {
                        var roomItem = new ListViewItem(room.Name) { Name = room.Guid.ToString() };
                        roomItem.SubItems.AddRange(new[]
                            {
                                room.Description, room.Users.Count.ToString(CultureInfo.InvariantCulture),
                                room.Private.ToString()
                            });
                        lstViewRooms.Items.Add(roomItem);
                    }
                }));
        }
        #endregion

        #region Form Event Handlers
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Disconnect();

            Invoke(new MethodInvoker(delegate
                {
                    ToggleTabPages(false);

                    Text = string.Format("{0} | {1}", Resources.BaseName, Resources.Connected);
                    SwitchToTab(tabPageRooms);

                    ClearListViews();

                    ClearRoomChat();

                    WriteToLog(Resources.ConnectionLost);
                }));
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (client.User.Socket != null && client.User.Socket.Connected) {
                ToggleConnectButtons(true);
            }

            if (client.User.Socket == null || !client.User.Socket.Connected) {
                ToggleConnectButtons(false);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            client.SendMessage(rchTxtInput.Text, client.User.Room);

            rchTxtInput.Clear();
        }

        private void rchTxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            // Only process enter key.
            if (e.KeyCode != Keys.Enter) return;

            btnSend_Click(this, null);
            // Silence beep.
            e.Handled = true;
        }


        // TODO
        private void createRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CreateRoom createRoom = new CreateRoom()) {
                if (createRoom.ShowDialog() != DialogResult.OK) return;

                client.CreateRoom(new NewRoom());
            }
        }

        private void lstViewRooms_DoubleClick(object sender, EventArgs e)
        {
            client.JoinServerRoom(Guid.Parse(lstViewRooms.SelectedItems[0].Name));
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.Connect();
        }
        #endregion

        #region Options Event Handlers
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OptionsForm options = new OptionsForm()) {
                if (options.ShowDialog() != DialogResult.OK) return;

                Options.IpAddress = IPAddress.Parse(options.IpAddress);
                Options.Port = int.Parse(options.Port);
                Options.Username = options.Username.Trim();

                Settings.Default.IPAddress = options.IpAddress;
                Settings.Default.Port = options.Port.ToString(CultureInfo.InvariantCulture);
                Settings.Default.Username = options.Username.Trim();

                Settings.Default.Save();
            }
        }
        #endregion
    }
}