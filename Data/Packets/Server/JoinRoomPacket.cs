using System;

namespace Data.Packets.Server
{
    [Serializable]
    public class JoinRoomPacket : Packet
    {
        public bool Allowed { get; set; }
        public Room Room { get; set; }

        public JoinRoomPacket(bool allowed, Room room)
        {
            Allowed = allowed;
            Room = room;
        }
    }
}
