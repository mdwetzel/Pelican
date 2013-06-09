using System;
using System.Runtime.Serialization;

namespace Data.Packets.Server
{
    [Serializable]
    public class BanNotificationPacket : Packet
    {
        public string Message { get; set; }

        public BanNotificationPacket(string message)
        {
            Message = message;
        }

        public BanNotificationPacket(SerializationInfo info, StreamingContext context)
        {
            Message = info.GetString("Message");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Message", Message);
        }
    }
}
