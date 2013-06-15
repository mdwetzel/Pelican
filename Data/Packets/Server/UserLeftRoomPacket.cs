#region Using
using System; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class UserLeftRoomPacket : Packet
    {
        #region Fields/Properties
        public string Message { get; set; }
        public User User { get; set; }
        public Room Room { get; set; } 
        #endregion

        #region Constructors
        public UserLeftRoomPacket(string message, User user, Room room)
        {
            Message = message;
            User = user;
            Room = room;
        } 
        #endregion
    }
}
