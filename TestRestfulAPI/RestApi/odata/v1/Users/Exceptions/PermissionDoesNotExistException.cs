using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Users.Exceptions
{
    [Serializable]
    internal class PermissionDoesNotExistException : DoesNotExistException
    {
        public PermissionDoesNotExistException()
        {
        }

        public PermissionDoesNotExistException(string message) : base(message)
        {
        }

        public PermissionDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PermissionDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}