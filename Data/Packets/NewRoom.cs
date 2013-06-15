﻿#region Using
using System;

#endregion
namespace Data.Packets
{
    [Serializable]
    public class NewRoom
    {
        #region Fields/Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdminPassword { get; set; }
        public string RoomPassword { get; set; }
        #endregion

        public NewRoom(string name, string description, string adminPassword, string roomPassword)
        {
            Name = name;
            Description = description;
            AdminPassword = adminPassword;
            RoomPassword = roomPassword;
        }
    }
}
