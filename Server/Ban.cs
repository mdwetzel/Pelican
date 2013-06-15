using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Server
{
    public class Ban
    {
        public IPAddress IP { get; set; }
        public DateTime Expiration { get; set; }

    }
}
