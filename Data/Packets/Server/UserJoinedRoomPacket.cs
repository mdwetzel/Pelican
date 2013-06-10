using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Packets.Server
{
    [Serializable]
    public class UserJoinedRoomPacket : Packet
    {
        public User User { get; set; }
        public Room Room { get; set; }
        public string Message { get; set; }

        public UserJoinedRoomPacket(string message, User user, Room room)
        {
            User = user;
            Room = room;
            Message = message;
        }
    }
}
