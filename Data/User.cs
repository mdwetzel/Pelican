using System;
using System.Net.Sockets;

namespace Data
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public Socket Socket { get; set; }
        public Guid Guid { get; set; }

        public User(string username)
        {
            Username = username;
            Guid = Guid.NewGuid();
        }
    }
}
