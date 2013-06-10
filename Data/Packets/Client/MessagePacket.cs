using System;

namespace Data.Packets.Client
{
    [Serializable]
    public class MessagePacket : Packet
    {
        public string Message { get; set; }
        public Room Room { get; set; }

        public MessagePacket(string message, Room room)
        {
            Message = message;
            Room = room;
        }
    }
}
