#region Using
using System;
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class CloseRoomPacket : Packet
    {
        #region Fields/Properties
        public string Message { get; set; }
        #endregion

        #region Constructors
        public CloseRoomPacket(string message)
        {
            Message = message;
        }
        #endregion
    }
}
