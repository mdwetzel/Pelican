#region Using
using System;
using System.Collections.Generic; 
#endregion

namespace Data.Packets.Server
{
    [Serializable]
    public class UpdateRoomsPacket : Packet
    {
        #region Fields/Properties
        public List<Room> Rooms { get; private set; } 
        #endregion

        #region Constructors
        public UpdateRoomsPacket(List<Room> rooms)
        {
            Rooms = rooms;
        } 
        #endregion
    }
}
