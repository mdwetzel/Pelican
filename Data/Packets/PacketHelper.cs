using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data.Packets
{
    public static class PacketHelper
    {
        public static byte[] Serialize(Packet packet)
        {
            using (MemoryStream memStream = new MemoryStream()) {
                BinaryFormatter binForm = new BinaryFormatter();
                binForm.Serialize(memStream, packet);

                return memStream.ToArray();
            }
        }

        public static Packet Deserialize(byte[] buffer)
        {
            using (MemoryStream memStream = new MemoryStream(buffer, 0, buffer.Length)) {
                BinaryFormatter binForm = new BinaryFormatter();

                Packet tempPacket = (Packet)binForm.Deserialize(memStream);

                return tempPacket;
            }
        }
    }
}
