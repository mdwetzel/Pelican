#region Using
using System.Net;
using Client.Properties;

#endregion

namespace Client
{
    public static class Options
    {
        #region Fields/Properties
        public static IPAddress IpAddress { get; set; }
        public static int Port { get; set; }
        #endregion

        #region Constructors
        static Options()
        {
            // Defaults.
            IpAddress = IPAddress.Parse(Settings.Default.IPAddress);
            Port = int.Parse(Settings.Default.Port);
        }
        #endregion
    }
}
