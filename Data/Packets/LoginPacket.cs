using System;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    public class LoginPacket : Packet
    {
        public string Username { get; set; }

        public LoginPacket(string username)
        {
            Username = username;
        }

        public LoginPacket(SerializationInfo info, StreamingContext context)
        {
            Username = info.GetString("Username");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Username", Username);
        }
    }
}
