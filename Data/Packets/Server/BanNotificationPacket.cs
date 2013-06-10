﻿using System;

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
    }
}
