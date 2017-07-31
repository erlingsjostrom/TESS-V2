using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Exceptions
{
    [Serializable]
    internal class RoleDoesNotExistException : DoesNotExistException
    {
        public RoleDoesNotExistException()
        {
        }

        public RoleDoesNotExistException(string message) : base(message)
        {
        }

        public RoleDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RoleDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}