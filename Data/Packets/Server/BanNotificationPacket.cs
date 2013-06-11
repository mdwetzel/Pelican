#region Using
using System;
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class BanNotificationPacket : Packet
    {
        #region Fields/Properties
        public string Message { get; set; } 
        #endregion

        #region Constructors
        public BanNotificationPacket(string message)
        {
            Message = message;
        } 
        #endregion
    }
}
