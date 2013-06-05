using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Data;

namespace Server
{
    public class Server
    {
        public Socket listenerSocket;

        public delegate void PacketReceivedHandler(StateObject state);

        public delegate void UserLoginHandler(LoginPacket packet, Socket socket);

        public delegate void CreateRoomHandler(CreateRoomPacket packet);

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

        public event ServerOfflineHandler ServerOffline;

        private bool online;

        public Server()
        {
            PacketReceived += Server_PacketReceived;
            UserLogin += Server_UserLogin;
            CreateRoom += Server_CreateRoom;
            UserDisconnected += Server_UserDisconnected;
            UserJoinRoom += Server_UserJoinRoom;


            rooms.Add(new Room { Name = "Markapolis", Description = "This is awesome.", Private = true });
        }

        void Server_UserJoinRoom(JoinRoomPacket packet, Socket socket)
        {
            User user = GetUser(socket);

            Room room = GetRoom(packet.Guid);

            if (!room.Users.Contains(user)) {
                room.Users.Add(user);
            }

            SendPacket(socket, PacketHelper.Serialize(new Data.Packets.Server.JoinRoomPacket(true)));

            SendPacket(socket, PacketHelper.Serialize(new UpdateRoomsPacket(rooms)));
        }

        public User GetUser(Socket socket)
        {
            return users.First(user => user.Socket == socket);
        }

        public User GetUser(Guid guid)
        {
            return users.First(user => user.Guid == guid);
        }

        public Room GetRoom(Guid guid)
        {
            return rooms.First(room => room.Guid == guid);
        }

        void Server_UserDisconnected(Guid guid)
        {
            users.Remove(GetUser(guid));
        }

        void Server_CreateRoom(CreateRoomPacket packet)
        {
            MessageBox.Show("Test");
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
                UserLogin(packet as LoginPacket, state.workSocket);
            } else if (packet is CreateRoomPacket) {
                CreateRoom(packet as CreateRoomPacket);
            } else if (packet is JoinRoomPacket) {
                UserJoinRoom(packet as JoinRoomPacket, state.workSocket);
            }
        }

        private void OnAccept(IAsyncResult ar)
        {
            try {
                StateObject state = new StateObject { workSocket = listenerSocket.EndAccept(ar) };

                listenerSocket.BeginAccept(OnAccept, null);

                state.workSocket.BeginReceive(state.Buffer, 0, StateObject.InitialBufferSize, SocketFlags.None, OnReceive, state);
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
                    UserDisconnected(GetUser(state.workSocket).Guid);
                }
            } catch (SocketException ex) {
                switch (ex.ErrorCode) {
                    case 10054:
                        UserDisconnected(GetUser(state.workSocket).Guid);
                        break;
                }
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
