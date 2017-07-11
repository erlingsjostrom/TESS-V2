using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.Infrastructure.Exceptions
{
    [Serializable]
    internal class ResourceMissingException : Exception
    {
        public ResourceMissingException()
        {
        }

        public ResourceMissingException(string message) : base(message)
        {
        }

        public ResourceMissingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}