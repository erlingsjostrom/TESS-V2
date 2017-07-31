using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Exceptions
{
    [Serializable]
    internal class ResourceAlreadyExistException : AlreadyExistException
    {
        public ResourceAlreadyExistException()
        {
        }

        public ResourceAlreadyExistException(string message) : base(message)
        {
        }

        public ResourceAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}