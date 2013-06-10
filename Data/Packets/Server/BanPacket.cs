﻿using System;

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
    }
}
