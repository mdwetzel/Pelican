using System;

namespace Data.Packets
{
    [Serializable]
    public class NewRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
