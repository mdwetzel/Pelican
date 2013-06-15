#region Using
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Client.Properties;
using Data;
using Data.Packets;
using Data.Packets.Client;
using Data.Packets.Server;
#endregion

namespace Client
{
    public class Client
    {
        #region Fields/Properties
        private readonly StateObject sendState = new StateObject();
        // This is you, the client user. 
        public User User { get; private set; }
        #endregion

        #region Delegates/Events
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

        public delegate void JoinRoomHandler(Data.Packets.Server.JoinRoomPacket packet);

        public event JoinRoomHandler JoinRoom;

        public delegate void RoomMessageHandler(RoomMessagePacket packet);

        public event RoomMessageHandler RoomMessage;

        public delegate void RefreshUsersHandler(RefreshUsersPacket packet);

        public event RefreshUsersHandler RefreshUsers;

        public delegate void UserJoinedRoomHandler(UserJoinedRoomPacket packet);

        public event UserJoinedRoomHandler UserJoinedRoom;

        public delegate void UserLeftRoomHandler(UserLeftRoomPacket packet);

        public event UserLeftRoomHandler UserLeftRoom;

        public delegate void BroadcastHandler(BroadcastPacket packet);

        public event BroadcastHandler Broadcast;
        #endregion

        #region Constructors
        public Client()
        {
            User = new User(Settings.Default.Username);

            PacketReceived += Client_PacketReceived;
            UpdateUserGuid += Client_UpdateUserGuid;
            ConnectionEstablished += Client_ConnectionEstablished;
            JoinRoom += Client_JoinRoom;

            // Connect to the server on startup. 
            Connect();
        }

        #endregion

        #region Client Event Handlers
        private void Client_JoinRoom(Data.Packets.Server.JoinRoomPacket packet)
        {
            User.Room = packet.Room;
        }

        // Begin receiving server data on successful connection.
        private void Client_ConnectionEstablished(StateObject state)
        {
            state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.InitialBufferSize, SocketFlags.None, OnReceive, state);
        }

        // Needed because the only GUIDs we should use should be created on the server. 
        private void Client_UpdateUserGuid(UpdateUserGuidPacket packet)
        {
            User.Guid = packet.Guid;
        }

        // Handle each unique packet and hand them off to whomever subscribes to them. 
        private void Client_PacketReceived(StateObject state)
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
            } else if (packet is Data.Packets.Server.JoinRoomPacket) {
                if (JoinRoom != null) JoinRoom(packet as Data.Packets.Server.JoinRoomPacket);
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
            } else if (packet is BroadcastPacket) {
                if (Broadcast != null) Broadcast(packet as BroadcastPacket);
            }
        }
        #endregion

        #region Asynchronous Methods
        private void OnReceive(IAsyncResult ar)
        {
            try {
                StateObject state = (StateObject)ar.AsyncState;
                int bytesReceived = state.WorkSocket.EndReceive(ar);

                if (bytesReceived > 0) {
                    // The upcoming packet's size. This is what we set the buffer size to. 
                    if (bytesReceived == sizeof(int)) {
                        state.Length = BitConverter.ToInt32(state.Buffer, 0);
                        state.Buffer = new byte[state.Length];
                    } else {
                        // If it's the actual packet data then simply read how many bytes we've received so far.
                        state.BytesReceived += bytesReceived;
                    }

                    // If we have a complete packet then process it. 
                    if (state.BytesReceived == state.Length) {
                        PacketReceived(state);

                        // Reset the state's...state.
                        state.Reset();
                    }

                    // If data's being received (bytesReceived != 0) then continue reading. 
                    state.WorkSocket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, OnReceive, state);
                } else {
                    // If bytesReceived == 0, then we've lost connection to the server.
                    // This is most likely a socket shutdown. 
                    if (ConnectionLost != null) ConnectionLost();
                }
            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    case 10054:
                        // Lost connection to server.
                        // This is probably the server-side form being closed. 
                        if (ConnectionLost != null) ConnectionLost();
                        break;
                }
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            state.WorkSocket.EndSend(ar);
        }

        private void OnConnect(IAsyncResult ar)
        {
            try {
                StateObject state = (StateObject)ar.AsyncState;
                state.WorkSocket.EndConnect(ar);

                // We're connected, but we need to send our username to the server. 
                SendPacket(PacketHelper.Serialize(new LoginPacket(User.Username)));

                if (ConnectionEstablished != null) ConnectionEstablished(state);

            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    // This is most likely that the server's closed or simply not listening for new connections. 
                    case 10061:
                        MessageBox.Show(Resources.ServerNotResponding);
                        break;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends a data packet to the server. 
        /// </summary>
        /// <param name="buffer"></param>
        private void SendPacket(byte[] buffer)
        {
            byte[] lengthPacket = BitConverter.GetBytes(buffer.Length);
            sendState.WorkSocket.Send(lengthPacket);
            sendState.WorkSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, sendState);
        }

        /// <summary>
        /// Disconnects the client from the server. 
        /// </summary>
        public void Disconnect()
        {
            User.Socket.Shutdown(SocketShutdown.Both);
        }

        /// <summary>
        /// Joins the server's specified chat room.
        /// </summary>
        /// <param name="guid">The GUID of the room to join.</param>
        public void JoinServerRoom(Guid guid)
        {
            SendPacket(PacketHelper.Serialize(new Data.Packets.Client.JoinRoomPacket(guid)));
        }

        /// <summary>
        /// Creates a new chat room on the server.
        /// </summary>
        /// <param name="newRoom">The new room to create.</param>
        public void CreateRoom(NewRoom newRoom)
        {
            CreateRoomPacket packet = new CreateRoomPacket(newRoom.Name, newRoom.Description);

            SendPacket(PacketHelper.Serialize(packet));
        }

        /// <summary>
        /// Sends a text message to the server's specified chat room.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="room">The room to send the message to.</param>
        public void SendMessage(string message, Room room)
        {
            SendPacket(PacketHelper.Serialize(new MessagePacket(message, room)));
        }

        /// <summary>
        /// Connects to the remote server.
        /// </summary>
        public void Connect()
        {
            if (User.Socket != null && User.Socket.Connected) return;

            StateObject state = new StateObject
            {
                WorkSocket = User.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            };

            sendState.WorkSocket = state.WorkSocket;
            state.WorkSocket.BeginConnect(new IPEndPoint(Options.IpAddress, Options.Port), OnConnect, state);
        }
        #endregion
    }
}
