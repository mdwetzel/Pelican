using System;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    public class UpdateUserGuidPacket : Packet
    {
        public Guid Guid { get; set; }

        public UpdateUserGuidPacket(Guid guid)
        {
            Guid = guid;
        }

        public UpdateUserGuidPacket(SerializationInfo info, StreamingContext context)
        {
            Guid = (Guid)info.GetValue("Guid", typeof(Guid));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Guid", Guid);
        }
    }
}
