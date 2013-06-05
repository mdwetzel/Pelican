using System;
using System.Runtime.Serialization;

namespace Data
{
    [Serializable]
    public abstract class Packet
    {
        protected Packet()
        {
        }

        protected Packet(SerializationInfo info, StreamingContext context)
        {
        }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
