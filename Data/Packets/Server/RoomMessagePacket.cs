using System;

namespace Data.Packets.Server
{
    [Serializable]
    public class RoomMessagePacket : Packet
    {
        public string Message { get; set; }

        public RoomMessagePacket(string message)
        {
            Message = message;
        }
    }
}
