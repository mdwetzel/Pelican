#region Using
using System; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class UserJoinedRoomPacket : Packet
    {
        #region Fields/Properties
        public User User { get; set; }
        public Room Room { get; set; }
        public string Message { get; set; } 
        #endregion

        #region Constructors
        public UserJoinedRoomPacket(string message, User user, Room room)
        {
            User = user;
            Room = room;
            Message = message;
        } 
        #endregion
    }
}
