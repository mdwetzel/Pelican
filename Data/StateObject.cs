#region Using
using System.Net.Sockets;
#endregion

namespace Data
{
    public class StateObject
    {
        #region Fields/Properties
        public Socket WorkSocket { get; set; }
        public const int InitialBufferSize = sizeof(int);
        public int Length;
        public int BytesReceived { get; set; }
        public byte[] Buffer = new byte[InitialBufferSize]; 
        #endregion

        #region Methods
        /// <summary>
        /// Resets the Buffer and BytesReceived to allow for a fresh packet read.
        /// </summary>
        public void Reset()
        {
            Buffer = new byte[StateObject.InitialBufferSize];
            BytesReceived = 0;
        } 
        #endregion
    }
}
