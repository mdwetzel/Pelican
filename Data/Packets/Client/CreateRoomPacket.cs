#region Using
using System;
#endregion

namespace Data.Packets.Client
{
    [Serializable]
    public class CreateRoomPacket : Packet
    {
        #region Fields/Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdminPassword { get; set; }
        public string RoomPassword { get; set; }
        #endregion

        #region Constructors
        public CreateRoomPacket(string name, string description, string adminPassword, string roomPassword)
        {
            Name = name;
            Description = description;
            AdminPassword = adminPassword;
            RoomPassword = roomPassword;
        }
        #endregion
    }
}
