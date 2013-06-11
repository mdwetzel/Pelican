#region Using
using System;
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class RoomMessagePacket : Packet
    {
        #region Fields/Properties
        public string Message { get; private set; } 
        #endregion

        #region Constructors
        public RoomMessagePacket(string message)
        {
            Message = message;
        } 
        #endregion
    }
}
