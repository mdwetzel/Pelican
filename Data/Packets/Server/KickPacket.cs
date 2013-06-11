#region Using
using System; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class KickPacket : Packet
    {
        #region Fields/Properties
        public Guid UserGuid { get; set; }
        public string TargetMessage { get; set; }
        public string RoomMessage { get; set; } 
        #endregion

        #region Constructors
        public KickPacket(Guid userGuid, string targetMessage, string roomMessage)
        {
            UserGuid = userGuid;
            TargetMessage = targetMessage;
            RoomMessage = roomMessage;
        } 
        #endregion
    }
}
