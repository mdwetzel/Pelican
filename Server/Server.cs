#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Data;
using Data.Packets;
using Data.Packets.Client;
using Data.Packets.Server;
using Server.Properties;
using ServerPackets = Data.Packets.Server;
using ClientPackets = Data.Packets.Client;
#endregion

namespace Server
{
    public class Server
    {
        #region Fields/Properties
        private Socket listenerSocket;
        private readonly List<Ban> bans = new List<Ban>();
        public readonly List<User> users = new List<User>();
        private List<Room> rooms = new List<Room>();
        public bool Online { get; private set; }
        #endregion

        #region Delegates/Events
        public delegate void PacketReceivedHandler(StateObject state);

        public delegate void UserLoginHandler(LoginPacket packet, Socket socket);

        public delegate void CreateRoomHandler(CreateRoomPacket packet);

        public event CreateRoomHandler CreateRoom;

        public event PacketReceivedHandler PacketReceived;

        public event UserLoginHandler UserLogin;

        public delegate void UserDisconnectHandler(Guid guid);

        public event UserDisconnectHandler UserDisconnected;

        public delegate void UserJoinRoomHandler(ClientPackets.JoinRoomPacket packet, Socket socket);

        public event UserJoinRoomHandler UserJoinRoom;

        public delegate void ServerOnlineHandler();

        public event ServerOnlineHandler ServerOnline;

        public delegate void ServerOfflineHandler();

        public delegate void UserMessageHandler(MessagePacket packet, Socket socket);

        public event UserMessageHandler UserMessage;

        public event ServerOfflineHandler ServerOffline;
        #endregion

        #region Constructors
        public Server()
        {
            PacketReceived += Server_PacketReceived;
            UserLogin += Server_UserLogin;
            UserDisconnected += Server_UserDisconnected;
            UserJoinRoom += Server_UserJoinRoom;
            UserMessage += Server_UserMessage;

            LoadRooms();
        }
        #endregion

        #region Server Event Handlers
        private void Server_UserMessage(MessagePacket packet, Socket socket)
        {
            User fromUser = GetUser(socket);
            Room userRoom = GetRoom(GetUser(socket).Room.Guid);

            foreach (var user in userRoom.Users) {
                SendPacket(user.Socket, PacketHelper.Serialize(new RoomMessagePacket(string.Format("{0}: {1}", fromUser.Username, packet.Message))));
            }
        }

        private void Server_UserJoinRoom(ClientPackets.JoinRoomPacket packet, Socket socket)
        {
            User user = GetUser(socket);
            Room room = GetRoom(packet.Guid);

            // The user's already in this room, so return.
            if (room.Users.Contains(user)) return;

            // Otherwise go ahead and proceed. 
            room.Users.Add(user);
            user.Room = room;

            // Send a packet to the user either allowing or disallowing joining the room.
            SendPacket(socket, PacketHelper.Serialize(new Data.Packets.Server.JoinRoomPacket(true, room)));

            // Send refresh and notification packets to all the users in the room. 
            foreach (var userItem in user.Room.Users) {
                SendPacket(userItem.Socket, PacketHelper.Serialize(new RefreshUsersPacket(room.Users)));
                SendPacket(userItem.Socket, PacketHelper.Serialize(new UserJoinedRoomPacket(string.Format("<<< {0} has joined {1} >>>", user.Username, room.Name), user, room)));
            }

            // Finally, send an update packet to reflect the new user count. 
            SendPacket(socket, PacketHelper.Serialize(new UpdateRoomsPacket(rooms)));
        }

        private void Server_UserDisconnected(Guid guid)
        {
            User user = GetUser(guid);
            Room room = user.Room;

            // The user doesn't have to be in a room before disconnecting, so check for this. 
            if (room != null) {
                room.Users.Remove(user);

                foreach (var userItem in room.Users) {
                    SendPacket(userItem.Socket, PacketHelper.Serialize(new RefreshUsersPacket(user.Room.Users)));
                    SendPacket(userItem.Socket, PacketHelper.Serialize(new UserLeftRoomPacket(string.Format("<<< {0} has left {1} >>>", user.Username, user.Room.Name), user, user.Room)));
                }
            }

            users.Remove(user);
        }

        private void Server_UserLogin(LoginPacket packet, Socket socket)
        {
            User newUser = new User(packet.Username) { Socket = socket };
            users.Add(newUser);

            SendPacket(socket, PacketHelper.Serialize(new UpdateUserGuidPacket(newUser.Guid)));
            SendPacket(socket, PacketHelper.Serialize(new UpdateRoomsPacket(rooms)));
        }

        private void Server_PacketReceived(StateObject state)
        {
            Packet packet = PacketHelper.Deserialize(state.Buffer);

            if (packet is LoginPacket) {
                if (UserLogin != null) UserLogin(packet as LoginPacket, state.WorkSocket);
            } else if (packet is CreateRoomPacket) {
                if (CreateRoom != null) CreateRoom(packet as CreateRoomPacket);
            } else if (packet is ClientPackets.JoinRoomPacket) {
                if (UserJoinRoom != null) UserJoinRoom(packet as ClientPackets.JoinRoomPacket, state.WorkSocket);
            } else if (packet is MessagePacket) {
                if (UserMessage != null) UserMessage(packet as MessagePacket, state.WorkSocket);
            }
        }
        #endregion

        #region Asynchronous Methods
        private void OnAccept(IAsyncResult ar)
        {
            try {
                StateObject state = new StateObject { WorkSocket = listenerSocket.EndAccept(ar) };

                if (bans.Find(ban => (Equals(ban.IP, ((IPEndPoint)state.WorkSocket.RemoteEndPoint).Address))) != null) {

                    SendPacket(state.WorkSocket, PacketHelper.Serialize(new BanNotificationPacket("You are banned.")));

                    state.WorkSocket.Shutdown(SocketShutdown.Both);
                } else {
                    state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.InitialBufferSize, SocketFlags.None, OnReceive, state);
                }

                listenerSocket.BeginAccept(OnAccept, null);

            } catch (ObjectDisposedException) {
                // We've stopped the server.
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            try {
                int bytesReceived = state.WorkSocket.EndReceive(ar);

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

                    state.WorkSocket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, OnReceive, state);
                } else {
                    User user = GetUser(state.WorkSocket);
                    if (user != null) {
                        UserDisconnected(user.Guid);
                    }
                }
            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    case 10054:
                        User user = GetUser(state.WorkSocket);
                        if (user != null) {
                            UserDisconnected(user.Guid);
                        }
                        break;
                }
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }
        #endregion

        #region Methods
        internal void Stop()
        {
            if (!Online) return;

            Online = false;

            // Stop listening for new connections. 
            listenerSocket.Close();

            // Disconnect all of the current users. 
            foreach (var user in users) {
                user.Socket.Shutdown(SocketShutdown.Both);
            }

            users.Clear();

            foreach (var room in rooms) {
                room.Users.Clear();
            }

            if (ServerOffline != null) ServerOffline();
        }

        internal void Start()
        {
            if (Online) return;

            Online = true;

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listenerSocket.Bind(new IPEndPoint(Configuration.IpAddress, Configuration.Port));
            listenerSocket.Listen(4);

            listenerSocket.BeginAccept(OnAccept, null);

            if (ServerOnline != null) ServerOnline();
        }

        private void DisconnectUser(User user)
        {
            users.Remove(user);
            user.Socket.Shutdown(SocketShutdown.Both);
        }

        internal void DisconnectUser(Socket socket)
        {
            users.Remove(GetUser(socket));
            socket.Shutdown(SocketShutdown.Both);
        }

        internal void DisconnectUser(Guid guid)
        {
            User user;
            users.Remove(user = GetUser(guid));
            user.Socket.Shutdown(SocketShutdown.Both);
        }

        /// <summary>
        /// Bans a user from the server and removes him from the room he was in. 
        /// </summary>
        /// <param name="user">The User to remove from the server.</param>
        internal void BanUser(User user)
        {
            SendPacket(user.Socket, PacketHelper.Serialize(new BanPacket(user.Guid, Resources.BannedMessage)));

            bans.Add(new Ban { IP = (((IPEndPoint)user.Socket.RemoteEndPoint).Address) });

            DisconnectUser(user);

            RemoveUserFromRoom(user);
        }

        /// <summary>
        /// Removes a user from the room and sends a RefreshUsersPacket to each remaining user in the room.
        /// </summary>
        /// <param name="user">The User to remove from the room.</param>
        private void RemoveUserFromRoom(User user)
        {
            if (user.Room == null) return;
            user.Room.Users.Remove(user);
            SendPacket(user.Room, PacketHelper.Serialize(new RefreshUsersPacket(user.Room.Users)));
        }

        /// <summary>
        /// Kicks a user from the server and removes him from the room he was in. 
        /// </summary>
        /// <param name="user">The User to kick from the server.</param>
        public void KickUser(User user)
        {
            if (user.Room == null) {
                SendPacket(user.Socket, PacketHelper.Serialize(new KickPacket(user.Guid, Resources.KickedMessage, null)));
            } else {
                SendPacket(user.Room, PacketHelper.Serialize(new KickPacket(user.Guid, Resources.KickedMessage, string.Format(Resources.KickedUserMessage, user.Username))));
            }

            DisconnectUser(user);

            RemoveUserFromRoom(user);
        }

        private void SendPacket(Socket socket, byte[] buffer)
        {
            byte[] lengthPacket = BitConverter.GetBytes(buffer.Length);
            socket.Send(lengthPacket);
            socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, socket);
        }

        private void SendPacket(Room room, byte[] buffer)
        {
            byte[] lengthPacket = BitConverter.GetBytes(buffer.Length);

            foreach (var user in room.Users) {
                user.Socket.Send(lengthPacket);
                user.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, user.Socket);
            }
        }

        /// <summary>
        /// Gets a user based on his socket.
        /// </summary>
        /// <param name="socket">The Socket of the User.</param>
        /// <returns></returns>
        public User GetUser(Socket socket)
        {
            try {
                return users.First(user => user.Socket == socket);
            } catch (InvalidOperationException) {
                return null;
            }
        }

        /// <summary>
        /// Gets a user based on his GUID.
        /// </summary>
        /// <param name="guid">The GUID of the User.</param>
        /// <returns></returns>
        public User GetUser(Guid guid)
        {
            try {
                return users.First(user => user.Guid == guid);
            } catch (InvalidOperationException) {
                return null;
            }
        }

        /// <summary>
        /// Gets a Room based on its GUID.
        /// </summary>
        /// <param name="guid">The GUID of the Room.</param>
        /// <returns></returns>
        public Room GetRoom(Guid guid)
        {
            return rooms.First(room => room.Guid == guid);
        }

        public void SaveRooms()
        {
            RoomsHelper.SerializeRooms(rooms, "rooms.xml");
        }

        private void LoadRooms()
        {
            rooms = RoomsHelper.DeserializeRooms("rooms.xml");
        }

        public void SendBroadcast(string text)
        {
            text = string.Format("<<< {0} >>>", text);

            foreach (var user in users) {
                SendPacket(user.Socket, PacketHelper.Serialize(new BroadcastPacket(text)));
            }
        }
        #endregion
    }
}
