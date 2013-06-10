using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Data
{
    public class StateObject
    {
        public Socket workSocket = null;

        public const int InitialBufferSize = sizeof(int);

        public int Length;

        public int BytesReceived { get; set; }

        public byte[] Buffer = new byte[InitialBufferSize];

        /// <summary>
        /// Resets the Buffer and BytesReceived to allow for a fresh packet read.
        /// </summary>
        public void Reset()
        {
            Buffer = new byte[StateObject.InitialBufferSize];
            BytesReceived = 0;
        }
    }
}
