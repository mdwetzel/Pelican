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
        #endregion

        #region Constructors
        public CreateRoomPacket(string name, string description)
        {
            Name = name;
            Description = description;
        } 
        #endregion
    }
}
