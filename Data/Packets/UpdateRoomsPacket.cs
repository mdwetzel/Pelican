using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    public class UpdateRoomsPacket : Packet
    {
        public List<Room> Rooms { get; set; }

        public UpdateRoomsPacket(List<Room> rooms)
        {
            Rooms = rooms;
        }

        public UpdateRoomsPacket(SerializationInfo info, StreamingContext context)
        {
            Rooms = (List<Room>)info.GetValue("Rooms", typeof(List<Room>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Rooms", Rooms);
        }
    }
}
