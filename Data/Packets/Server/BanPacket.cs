using System;
using System.Runtime.Serialization;

namespace Data.Packets.Server
{
    [Serializable]
    public class BanPacket : Packet
    {
        public Guid Guid { get; set; }
        public string Message { get; set; }

        public BanPacket(Guid guid, string message)
        {
            Guid = guid;
            Message = message;
        }

        public BanPacket(SerializationInfo info, StreamingContext context)
        {
            Guid = (Guid)info.GetValue("Guid", typeof(Guid));
            Message = info.GetString("Message");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Guid", Guid);
            info.AddValue("Message", Message);
        }
    }
}
