#region Using
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization; 
#endregion

namespace Data
{
    public static class RoomsHelper
    {
        #region Methods
        public static void SerializeRooms(List<Room> rooms, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));

            serializer.Serialize(new StreamWriter(filename), rooms);
        }

        public static List<Room> DeserializeRooms(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));

            return (List<Room>)serializer.Deserialize(new StreamReader(filename));
        } 
        #endregion
    }
}