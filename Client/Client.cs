using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Data;
using Data.Packets;
using Data.Packets.Client;
using Data.Packets.Server;
using ServerPackets = Data.Packets.Server;

namespace Client
{
    public class Client
    {
        private readonly StateObject sendState = new StateObject();
        private readonly User user = new User("Test");

        public User User
        {
            get { return user; }
        }

        public delegate void PacketReceivedHandler(StateObject state);

        public event PacketReceivedHandler PacketReceived;

        public delegate void UpdateRoomsPacket(Data.Packets.Server.UpdateRoomsPacket packet);

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

        public delegate void JoinRoomHandler(ServerPackets.JoinRoomPacket packet);

        public event JoinRoomHandler JoinRoom;

        public delegate void RoomMessageHandler(RoomMessagePacket packet);

        public event RoomMessageHandler RoomMessage;

        public delegate void RefreshUsersHandler(RefreshUsersPacket packet);

        public event RefreshUsersHandler RefreshUsers;

        public delegate void UserJoinedRoomHandler(UserJoinedRoomPacket packet);

        public event UserJoinedRoomHandler UserJoinedRoom;

        public delegate void UserLeftRoomHandler(UserLeftRoomPacket packet);

        public event UserLeftRoomHandler UserLeftRoom;

        public Client()
        {
            PacketReceived += Client_PacketReceived;
            UpdateUserGuid += Client_UpdateUserGuid;
            KickUser += Client_KickUser;
            BanUser += Client_BanUser;
            ConnectionEstablished += Client_ConnectionEstablished;
            RefreshUsers += Client_RefreshUsers;
            UserLeftRoom += Client_UserLeftRoom;
            JoinRoom += Client_JoinRoom;

            RoomMessage += Client_RoomMessage;

            Connect();
        }

        void Client_JoinRoom(ServerPackets.JoinRoomPacket packet)
        {
            User.Room = packet.Room;
        }

        void Client_UserLeftRoom(UserLeftRoomPacket packet)
        {
        }

        void Client_RefreshUsers(RefreshUsersPacket packet)
        {

        }

        void Client_RoomMessage(RoomMessagePacket packet)
        {
        }

        void Client_ConnectionEstablished(StateObject state)
        {

            state.workSocket.BeginReceive(state.Buffer, 0, StateObject.InitialBufferSize, SocketFlags.None,
                                          OnReceive, state);
        }

        void Client_BanUser(BanPacket packet)
        {

        }

        public void JoinServerRoom(Guid guid)
        {
            SendPacket(PacketHelper.Serialize(new Data.Packets.Client.JoinRoomPacket(guid)));
        }

        public void CreateRoom(NewRoom newRoom)
        {
            CreateRoomPacket packet = new CreateRoomPacket(newRoom.Name, newRoom.Description);

            SendPacket(PacketHelper.Serialize(packet));
        }

        public void SendMessage(string message, Room room)
        {
            SendPacket(PacketHelper.Serialize(new MessagePacket(message, room)));
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

            if (packet is Data.Packets.Server.UpdateRoomsPacket) {
                if (UpdateRooms != null) UpdateRooms(packet as Data.Packets.Server.UpdateRoomsPacket);
            } else if (packet is KickPacket) {
                if (KickUser != null) KickUser(packet as KickPacket);
            } else if (packet is BanPacket) {
                if (BanUser != null) BanUser(packet as BanPacket);
            } else if (packet is UpdateUserGuidPacket) {
                if (UpdateUserGuid != null) UpdateUserGuid(packet as UpdateUserGuidPacket);
            } else if (packet is ServerPackets.JoinRoomPacket) {
                if (JoinRoom != null) JoinRoom(packet as ServerPackets.JoinRoomPacket);
            } else if (packet is BanNotificationPacket) {
                if (BanNotification != null) BanNotification(packet as BanNotificationPacket);
            } else if (packet is RoomMessagePacket) {
                if (RoomMessage != null) RoomMessage(packet as RoomMessagePacket);
            } else if (packet is RefreshUsersPacket) {
                if (RefreshUsers != null) RefreshUsers(packet as RefreshUsersPacket);
            } else if (packet is UserJoinedRoomPacket) {
                if (UserJoinedRoom != null) UserJoinedRoom(packet as UserJoinedRoomPacket);
            } else if (packet is UserLeftRoomPacket) {
                if (UserLeftRoom != null) UserLeftRoom(packet as UserLeftRoomPacket);
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

        private void SendPacket(byte[] buffer)
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
