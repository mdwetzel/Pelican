#region Using
using System;
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class BroadcastPacket : Packet
    {
        #region Fields/Properties
        public string Message { get; set; }
        #endregion

        #region Constructors
        public BroadcastPacket(string message)
        {
            Message = message;
        }
        #endregion
    }
}
