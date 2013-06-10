using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Packets.Server
{
    [Serializable]
    public class RefreshUsersPacket : Packet
    {
        private List<User> users;

        public List<User> Users { get; set; }

        public RefreshUsersPacket(List<User> users)
        {
            Users = users;
        }
    }
}
