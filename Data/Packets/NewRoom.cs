#region Using
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
        #endregion
    }
}
