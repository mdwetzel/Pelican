#region Using
using System;
using System.Collections.Generic; 
#endregion

namespace Data
{
    [Serializable]
    public class Room
    {
        #region Fields/Properties
        public List<User> Users { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
        public Guid Guid { get; private set; } 
        #endregion

        #region Constructors
        public Room()
        {
            Users = new List<User>();
            Guid = Guid.NewGuid();
        } 
        #endregion
    }
}
