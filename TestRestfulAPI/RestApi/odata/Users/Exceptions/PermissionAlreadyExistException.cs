using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.Users.Exceptions
{
    [Serializable]
    internal class PermissionAlreadyExistException : AlreadyExistException
    {
        public PermissionAlreadyExistException()
        {
        }

        public PermissionAlreadyExistException(string message) : base(message)
        {
        }

        public PermissionAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PermissionAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}