#region Using
using System; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class JoinRoomPacket : Packet
    {
        #region Fields/Properties
        public bool Allowed { get; set; }
        public Room Room { get; set; } 
        #endregion

        #region Constructors
        public JoinRoomPacket(bool allowed, Room room)
        {
            Allowed = allowed;
            Room = room;
        } 
        #endregion
    }
}
