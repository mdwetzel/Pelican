using System;
using System.Runtime.Serialization;

namespace Data.Packets
{
    [Serializable]
    public class KickPacket : Packet
    {
        public Guid UserGuid { get; set; }

        public KickPacket(Guid userGuid)
        {
            UserGuid = userGuid;
        }

        public KickPacket(SerializationInfo info, StreamingContext context)
        {
            UserGuid = (Guid) info.GetValue("Guid", typeof (Guid));
        }


        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Guid", UserGuid);
        }
    }
}
