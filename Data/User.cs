#region Using
using System;
using System.Net.Sockets;
#endregion

namespace Data
{
    [Serializable]
    public class User
    {
        #region Fields/Properties
        [NonSerialized]
        private Socket socket;
        public Room Room { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; private set; }

        public Socket Socket
        {
            get { return socket; }
            set { socket = value; }
        } 
        #endregion

        #region Constructors
        public User(string username)
        {
            Username = username;
            Guid = Guid.NewGuid();
        } 
        #endregion
    }
}
