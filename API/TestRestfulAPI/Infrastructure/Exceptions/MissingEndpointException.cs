using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.Infrastructure.Exceptions
{
    [Serializable]
    internal class MissingEndpointException : Exception
    {
        public MissingEndpointException()
        {
        }

        public MissingEndpointException(string message) : base(message)
        {
        }

        public MissingEndpointException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}