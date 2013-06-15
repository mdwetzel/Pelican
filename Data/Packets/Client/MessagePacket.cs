#region Using
using System;

#endregion
namespace Data.Packets.Client
{
    [Serializable]
    public class MessagePacket : Packet
    {
        #region Fields/Properties
        public string Message { get; private set; }
        public Room Room { get; set; } 
        #endregion

        #region Constructors
        public MessagePacket(string message, Room room)
        {
            Message = message;
            Room = room;
        } 
        #endregion
    }
}
