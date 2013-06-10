#region Using
using System.Net;
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
            IpAddress = IPAddress.Loopback;
            Port = 1000;
        }
        #endregion
    }
}
