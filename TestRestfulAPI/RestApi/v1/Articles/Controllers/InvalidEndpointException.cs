using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.RestApi.v1.Articles.Controllers
{
    [Serializable]
    internal class InvalidEndpointException : Exception
    {
        public InvalidEndpointException()
        {
        }

        public InvalidEndpointException(string message) : base(message)
        {
        }

        public InvalidEndpointException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}