using System;

namespace Data.Packets.Client
{
    [Serializable]
    public class JoinRoomPacket : Packet
    {
        public Guid Guid { get; set; }

        public JoinRoomPacket(Guid guid)
        {
            Guid = guid;
        }
    }
}
