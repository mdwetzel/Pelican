using System;
using System.Windows.Forms;
using Data;
using Data.Packets;

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

            client.ConnectionEstablished += client_ConnectionEstablished;

        }

        void client_KickUser(KickPacket packet)
        {
            if (packet.UserGuid == client.User.Guid) {
                Invoke(new MethodInvoker(() => MessageBox.Show("You have been kicked.")));
            }
        }

        void client_ConnectionEstablished()
        {
            Invoke(new MethodInvoker(delegate
            {
                tabPageRoom.Enabled = true;
                tabPageRooms.Enabled = true;

                Text = "Pelican";
                tabControl1.SelectedTab = tabPageRooms;
            }));
        }

        private void client_ConnectionLost()
        {
            Invoke(new MethodInvoker(delegate
            {
                tabPageRoom.Enabled = false;
                tabPageRooms.Enabled = false;

                Text = "Pelican - Disconnected";
                tabControl1.SelectedTab = tabPageRooms;

                client.User.Socket = null;

                if (MessageBox.Show("Reconnect?", "Reconnect?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    client.Connect();
                }
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

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void rchTxtInput_KeyDown(object sender, KeyEventArgs e)
        {

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
                tabControl1.SelectedTab = tabPageRooms;
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

    }
}
