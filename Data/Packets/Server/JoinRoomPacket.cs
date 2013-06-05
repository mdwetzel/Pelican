using System;
using System.Runtime.Serialization;

namespace Data.Packets.Server
{
    [Serializable]
    public class JoinRoomPacket : Packet
    {
        public bool Allowed { get; set; }

        public JoinRoomPacket(bool allowed)
        {
            Allowed = allowed;
        }

        public JoinRoomPacket(SerializationInfo info, StreamingContext context)
        {
            Allowed = info.GetBoolean("Allowed");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
           info.AddValue("Allowed", Allowed);
        }
    }
}
