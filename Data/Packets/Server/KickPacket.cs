using System;

namespace Data.Packets.Server
{
    [Serializable]
    public class KickPacket : Packet
    {
        public Guid UserGuid { get; set; }
        public string TargetMessage { get; set; }
        public string RoomMessage { get; set; }

        public KickPacket(Guid userGuid, string targetMessage, string roomMessage)
        {
            UserGuid = userGuid;
            TargetMessage = targetMessage;
            RoomMessage = roomMessage;
        }
    }
}
