#region Using
using System; 
#endregion

namespace Data.Packets.Client
{
    [Serializable]
    public class LoginPacket : Packet
    {
        #region Fields/Properties
        public string Username { get; set; } 
        #endregion

        #region Constructors
        public LoginPacket(string username)
        {
            Username = username;
        } 
        #endregion
    }
}
