#region Using
using System; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class UpdateUserGuidPacket : Packet
    {
        #region Fields/Properties
        public Guid Guid { get; private set; } 
        #endregion

        #region Constructors
        public UpdateUserGuidPacket(Guid guid)
        {
            Guid = guid;
        } 
        #endregion
    }
}
