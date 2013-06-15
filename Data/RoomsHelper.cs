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
            using (var writer = new StreamWriter(filename)) {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));

                serializer.Serialize(writer, rooms);

                writer.Close();
            }
        }

        public static List<Room> DeserializeRooms(string filename)
        {
            using (StreamReader reader = new StreamReader(filename)) {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));

                return (List<Room>)serializer.Deserialize(reader);
            }
        }
        #endregion
    }
}