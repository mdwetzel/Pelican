using System;

namespace Data.Packets.Client
{
    [Serializable]
    public class LoginPacket : Packet
    {
        public string Username { get; set; }

        public LoginPacket(string username)
        {
            Username = username;
        }
    }
}
