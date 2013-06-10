using System;

namespace Data.Packets.Server
{
    [Serializable]
    public class UpdateUserGuidPacket : Packet
    {
        public Guid Guid { get; set; }

        public UpdateUserGuidPacket(Guid guid)
        {
            Guid = guid;
        }
    }
}
