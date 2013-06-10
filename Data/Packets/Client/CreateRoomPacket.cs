using System;

namespace Data.Packets.Client
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
    }
}
