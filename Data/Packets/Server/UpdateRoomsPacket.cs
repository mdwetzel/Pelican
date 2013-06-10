using System;
using System.Collections.Generic;

namespace Data.Packets.Server
{
    [Serializable]
    public class UpdateRoomsPacket : Packet
    {
        public List<Room> Rooms { get; set; }

        public UpdateRoomsPacket(List<Room> rooms)
        {
            Rooms = rooms;
        }
    }
}
