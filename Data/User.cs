using System;
using System.Net.Sockets;

namespace Data
{
    [Serializable]
    public class User
    {
        [NonSerialized]
        private Socket socket;

        public Room Room { get; set; }
        public string Username { get; set; }

        public Socket Socket
        {
            get { return socket; }
            set { socket = value; }
        }

        public Guid Guid { get; set; }

        public User(string username)
        {
            Username = username;
            Guid = Guid.NewGuid();
        }
    }
}
