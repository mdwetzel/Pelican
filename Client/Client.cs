using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Data;
using Data.Packets;
using Data.Packets.Server;

namespace Client
{
    public class Client
    {
        readonly StateObject sendState = new StateObject();
        private readonly User user = new User("Test");

        public User User
        {
            get { return user; }
        }

        public delegate void PacketReceivedHandler(StateObject state);

        public event PacketReceivedHandler PacketReceived;

        public delegate void UpdateRoomsPacket(Data.UpdateRoomsPacket packet);

        public event UpdateRoomsPacket UpdateRooms;

        public delegate void ConnectionLostHandler();

        public event ConnectionLostHandler ConnectionLost;

        public delegate void ConnectionEstablishedHandler(StateObject state);

        public event ConnectionEstablishedHandler ConnectionEstablished;

        public delegate void UpdateUserGuidPacketHandler(UpdateUserGuidPacket packet);

        public event UpdateUserGuidPacketHandler UpdateUserGuid;

        public delegate void KickUserHandler(KickPacket packet);

        public event KickUserHandler KickUser;

        public delegate void BanUserHandler(BanPacket packet);

        public event BanUserHandler BanUser;

        public delegate void BanNotificationHandler(BanNotificationPacket packet);

        public event BanNotificationHandler BanNotification;

        public Client()
        {
            PacketReceived += Client_PacketReceived;
            UpdateUserGuid += Client_UpdateUserGuid;
            KickUser += Client_KickUser;
            BanUser += Client_BanUser;
            ConnectionEstablished += new ConnectionEstablishedHandler(Client_ConnectionEstablished);

            Connect();
        }

        void Client_ConnectionEstablished(StateObject state)
        {

            state.workSocket.BeginReceive(state.Buffer, 0, StateObject.InitialBufferSize, SocketFlags.None,
                                          OnReceive, state);
        }

        void Client_BanUser(BanPacket packet)
        {

        }

        public void Connect()
        {
            if (user.Socket != null && user.Socket.Connected) return;

            StateObject state = new StateObject
            {
                workSocket = user.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            };

            sendState.workSocket = state.workSocket;
            state.workSocket.BeginConnect(new IPEndPoint(IPAddress.Loopback, 1000), OnConnect, state);
        }

        void Client_KickUser(KickPacket packet)
        {

        }

        void Client_UpdateUserGuid(UpdateUserGuidPacket packet)
        {
            user.Guid = packet.Guid;
        }

        void Client_PacketReceived(StateObject state)
        {
            Packet packet = PacketHelper.Deserialize(state.Buffer);

            if (packet is Data.UpdateRoomsPacket) {
                if (UpdateRooms != null) UpdateRooms(packet as Data.UpdateRoomsPacket);
            } else if (packet is KickPacket) {
                if (KickUser != null) KickUser(packet as KickPacket);
            } else if (packet is BanPacket) {
                if (BanUser != null) BanUser(packet as BanPacket);
            } else if (packet is UpdateUserGuidPacket) {
                if (UpdateUserGuid != null) UpdateUserGuid(packet as UpdateUserGuidPacket);
            } else if (packet is Data.Packets.Server.JoinRoomPacket) {
                packet = packet as Data.Packets.Server.JoinRoomPacket;
            } else if (packet is Data.Packets.Server.BanNotificationPacket) {

                BanNotification(packet as Data.Packets.Server.BanNotificationPacket);
            }
        }

        private void OnConnect(IAsyncResult ar)
        {
            try {
                StateObject state = (StateObject)ar.AsyncState;
                state.workSocket.EndConnect(ar);

                SendPacket(PacketHelper.Serialize(new LoginPacket(user.Username)));

                if (ConnectionEstablished != null) ConnectionEstablished(state);

            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    case 10061:
                        MessageBox.Show("The server is currently not responding. Please try again later.");
                        break;
                }
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try {
                StateObject state = (StateObject)ar.AsyncState;
                int bytesReceived = state.workSocket.EndReceive(ar);

                if (bytesReceived > 0) {
                    if (bytesReceived == sizeof(int)) {
                        state.Length = BitConverter.ToInt32(state.Buffer, 0);
                        state.Buffer = new byte[state.Length];
                    } else {
                        state.BytesReceived += bytesReceived;
                    }

                    if (state.BytesReceived == state.Length) {
                        PacketReceived(state);

                        state.Buffer = new byte[StateObject.InitialBufferSize];
                        state.BytesReceived = 0;
                    }

                    state.workSocket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, OnReceive, state);
                } else {
                    // Lost connection to the server.
                    if (ConnectionLost != null) ConnectionLost();
                }
            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    case 10054:
                        // Lost connection to server.
                        if (ConnectionLost != null) ConnectionLost();
                        break;
                }
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            state.workSocket.EndSend(ar);
        }

        public void SendPacket(byte[] buffer)
        {
            byte[] lengthPacket = BitConverter.GetBytes(buffer.Length);
            sendState.workSocket.Send(lengthPacket);
            sendState.workSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, sendState);
        }

        internal void Disconnect()
        {
            user.Socket.Shutdown(SocketShutdown.Both);
        }
    }
}
