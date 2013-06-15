#region Using
using System.Net;
using Server.Properties; 
#endregion

namespace Server
{
    public static class Configuration
    {
        #region Fields/Properties
        public static IPAddress IpAddress { get; set; }
        public static int Port { get; set; } 
        #endregion

        #region Constructors
        static Configuration()
        {
            IpAddress = IPAddress.Parse(Settings.Default.IpAddress);
            Port = int.Parse(Settings.Default.Port);
        } 
        #endregion
    }
}
