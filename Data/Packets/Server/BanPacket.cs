#region Using
using System; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class BanPacket : Packet
    {
        #region Fields/Properties
        public Guid Guid { get; set; }
        public string Message { get; set; } 
        #endregion

        #region Constructors
        public BanPacket(Guid guid, string message)
        {
            Guid = guid;
            Message = message;
        } 
        #endregion
    }
}
