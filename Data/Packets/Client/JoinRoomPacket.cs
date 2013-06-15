#region Using
using System; 
#endregion

namespace Data.Packets.Client
{
    [Serializable]
    public class JoinRoomPacket : Packet
    {
        #region Fields/Properties
        public Guid Guid { get; set; } 
        #endregion

        #region Constructors
        public JoinRoomPacket(Guid guid)
        {
            Guid = guid;
        }
        #endregion
    }
}
