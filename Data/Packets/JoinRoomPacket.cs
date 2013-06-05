using System;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    public class JoinRoomPacket : Packet
    {
        public Guid Guid { get; set; }

        public JoinRoomPacket(Guid guid)
        {
            Guid = guid;
        }

        public JoinRoomPacket(SerializationInfo info, StreamingContext context)
        {
            Guid = (Guid)info.GetValue("Guid", typeof(Guid));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Guid", Guid);
        }
    }
}
