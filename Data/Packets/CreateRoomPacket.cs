using System;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    public class CreateRoomPacket : Packet
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateRoomPacket(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public CreateRoomPacket(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Description = info.GetString("Description");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
        }
    }
}
