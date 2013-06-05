using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class Room
    {
        [NonSerialized]
        private List<User> users = new List<User>();
        public List<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
        public Guid Guid { get; set; }

        public Room()
        {
            Users = new List<User>();
            Guid = Guid.NewGuid();
        }
    }
}
