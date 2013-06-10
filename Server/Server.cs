using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Data;
using Data.Packets;
using Data.Packets.Server;
using MS.Internal.Xml.XPath;
using JoinRoomPacket = Data.Packets.Client.JoinRoomPacket;
using ServerPackets = Data.Packets.Server;
using ClientPackets = Data.Packets.Client;

namespace Server
{
    public class Server
    {
        public Socket listenerSocket;

        public delegate void PacketReceivedHandler(StateObject state);

        public delegate void UserLoginHandler(ClientPackets.LoginPacket packet, Socket socket);

        public delegate void CreateRoomHandler(ClientPackets.CreateRoomPacket packet);

        public event CreateRoomHandler CreateRoom;

        public event PacketReceivedHandler PacketReceived;

        public event UserLoginHandler UserLogin;

        public delegate void UserDisconnectHandler(Guid guid);

        public event UserDisconnectHandler UserDisconnected;

        public List<User> users = new List<User>();

        public List<Room> rooms = new List<Room>();

        public delegate void UserJoinRoomHandler(JoinRoomPacket packet, Socket socket);

        public event UserJoinRoomHandler UserJoinRoom;

        public delegate void ServerOnlineHandler();

        public event ServerOnlineHandler ServerOnline;

        public delegate void ServerOfflineHandler();

        public delegate void UserMessageHandler(ClientPackets.MessagePacket packet, Socket socket);

        public event UserMessageHandler UserMessage;




        public event ServerOfflineHandler ServerOffline;

        private bool online;

        public List<Ban> bans = new List<Ban>();

        public Server()
        {
            PacketReceived += Server_PacketReceived;
            UserLogin += Server_UserLogin;
            CreateRoom += Server_CreateRoom;
            UserDisconnected += Server_UserDisconnected;
            UserJoinRoom += Server_UserJoinRoom;
            UserMessage += new UserMessageHandler(Server_UserMessage);

            rooms.Add(new Room { Name = "Markapolis", Description = "This is awesome.", Private = true });
        }

        void Server_UserMessage(ClientPackets.MessagePacket packet, Socket socket)
        {
            Room room = GetRoom(GetUser(socket).Room.Guid);

            User from = GetUser(socket);

            foreach (var user in room.Users) {
                SendPacket(user.Socket, PacketHelper.Serialize(new RoomMessagePacket(string.Format("{0}: {1}", from.Username, packet.Message))));
            }
        }

        void Server_UserJoinRoom(JoinRoomPacket packet, Socket socket)
        {
            User user = GetUser(socket);

            Room room = GetRoom(packet.Guid);

            if (!room.Users.Contains(user)) {
                room.Users.Add(user);

                user.Room = room;

                SendPacket(socket, PacketHelper.Serialize(new Data.Packets.Server.JoinRoomPacket(true, room)));


                foreach (var userItem in user.Room.Users) {
                    SendPacket(userItem.Socket, PacketHelper.Serialize(new RefreshUsersPacket(room.Users)));
                    SendPacket(userItem.Socket, PacketHelper.Serialize(new UserJoinedRoomPacket(string.Format("<<< {0} has joined {1} >>>", user.Username, room.Name), user, room)));
                }

                SendPacket(socket, PacketHelper.Serialize(new UpdateRoomsPacket(rooms)));
            }
        }

        public User GetUser(Socket socket)
        {
            try {
                return users.First(user => user.Socket == socket);
            } catch (InvalidOperationException) {
                return null;
            }
        }

        public User GetUser(Guid guid)
        {
            try {
                return users.First(user => user.Guid == guid);
            } catch (InvalidOperationException) {
                return null;
            }
        }

        public Room GetRoom(Guid guid)
        {
            return rooms.First(room => room.Guid == guid);
        }

        void Server_UserDisconnected(Guid guid)
        {
            User user = GetUser(guid);
            Room room = user.Room;


            room.Users.Remove(user);

            foreach (var userItem in room.Users) {
                SendPacket(userItem.Socket,
                           PacketHelper.Serialize(
                               new UserLeftRoomPacket(
                                   string.Format("<<< {0} has left {1} >>>", user.Username, user.Room.Name), user,
                                   user.Room)));


                SendPacket(userItem.Socket, PacketHelper.Serialize(new RefreshUsersPacket(user.Room.Users)));

            }

            users.Remove(user);


        }

        void Server_CreateRoom(ClientPackets.CreateRoomPacket packet)
        {
            MessageBox.Show("Test");
        }

        private void Server_UserLogin(ClientPackets.LoginPacket packet, Socket socket)
        {
            User newUser = new User(packet.Username) { Socket = socket };
            users.Add(newUser);

            SendPacket(socket, PacketHelper.Serialize(new UpdateUserGuidPacket(newUser.Guid)));
            SendPacket(socket, PacketHelper.Serialize(new UpdateRoomsPacket(rooms)));
        }

        private void Server_PacketReceived(StateObject state)
        {
            Packet packet = PacketHelper.Deserialize(state.Buffer);

            if (packet is ClientPackets.LoginPacket) {
                if (UserLogin != null) UserLogin(packet as ClientPackets.LoginPacket, state.workSocket);
            } else if (packet is ClientPackets.CreateRoomPacket) {
                if (CreateRoom != null) CreateRoom(packet as ClientPackets.CreateRoomPacket);
            } else if (packet is JoinRoomPacket) {
                if (UserJoinRoom != null) UserJoinRoom(packet as JoinRoomPacket, state.workSocket);
            } else if (packet is ClientPackets.MessagePacket) {
                if (UserMessage != null) UserMessage(packet as ClientPackets.MessagePacket, state.workSocket);
            }
        }

        private void OnAccept(IAsyncResult ar)
        {
            try {
                StateObject state = new StateObject { workSocket = listenerSocket.EndAccept(ar) };

                if (bans.Find(ban => (Equals(ban.IP, ((IPEndPoint)state.workSocket.RemoteEndPoint).Address))) != null) {

                    SendPacket(state.workSocket, PacketHelper.Serialize(new Data.Packets.Server.BanNotificationPacket("You are banned.")));

                    state.workSocket.Shutdown(SocketShutdown.Both);
                } else {
                    state.workSocket.BeginReceive(state.Buffer, 0, StateObject.InitialBufferSize, SocketFlags.None, OnReceive, state);
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
                    User user = GetUser(state.workSocket);
                    if (user != null) {
                        UserDisconnected(user.Guid);
                    }
                }
            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    case 10054:
                        User user = GetUser(state.workSocket);
                        if (user != null) {
                            UserDisconnected(user.Guid);
                        }
                        break;
                }
            }
        }

        public void SendPacket(Room room, byte[] buffer)
        {
            byte[] lengthPacket = BitConverter.GetBytes(buffer.Length);

            foreach (var user in room.Users) {
                user.Socket.Send(lengthPacket);
                user.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, user.Socket);
            }

        }

        public void SendPacket(Socket socket, byte[] buffer)
        {

            byte[] lengthPacket = BitConverter.GetBytes(buffer.Length);
            socket.Send(lengthPacket);
            socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSend, socket);
        }

        private void OnSend(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }

        internal void Stop()
        {
            if (!online) return;

            online = false;

            // Stop listening for new connections. 
            listenerSocket.Close();

            // Disconnect all of the current users. 
            foreach (var user in users) {
                user.Socket.Shutdown(SocketShutdown.Both);
            }

            foreach (var room in rooms) {
                room.Users.Clear();
            }

            if (ServerOffline != null) ServerOffline();
        }

        internal void Start()
        {
            if (online) return;

            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listenerSocket.Bind(new IPEndPoint(IPAddress.Loopback, 1000));
            listenerSocket.Listen(4);

            online = true;

            listenerSocket.BeginAccept(OnAccept, null);

            ServerOnline();
        }
    }
}
