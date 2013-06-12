#region Using
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace Data
{
    [Serializable]
    public class Room
    {
        #region Fields/Properties
        [XmlIgnore]
        public List<User> Users { get; private set; }
        [XmlIgnore]
        public Guid Guid { get; private set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
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
