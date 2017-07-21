using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Exceptions
{
    [Serializable]
    internal class ResourceDoesNotExistException : DoesNotExistException
    {
        public ResourceDoesNotExistException()
        {
        }

        public ResourceDoesNotExistException(string message) : base(message)
        {
        }

        public ResourceDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}