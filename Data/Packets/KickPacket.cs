﻿using System;
using System.Runtime.Serialization;

namespace Data.Packets
{
    [Serializable]
    public class KickPacket : Packet
    {
        public Guid UserGuid { get; set; }
        public string Message { get; set; }

        public KickPacket(Guid userGuid, string message)
        {
            UserGuid = userGuid;
            Message = message;
        }

        public KickPacket(SerializationInfo info, StreamingContext context)
        {
            UserGuid = (Guid)info.GetValue("Guid", typeof(Guid));
            Message = info.GetString("Message");
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Guid", UserGuid);
            info.AddValue("Message", Message);
        }
    }
}
