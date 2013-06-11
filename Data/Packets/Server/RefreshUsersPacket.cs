#region Using
using System;
using System.Collections.Generic; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class RefreshUsersPacket : Packet
    {
        #region Fields/Properties
        public List<User> Users { get; private set; } 
        #endregion

        #region Constructors
        public RefreshUsersPacket(List<User> users)
        {
            Users = users;
        } 
        #endregion
    }
}
