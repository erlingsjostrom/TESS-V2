using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.Users.Exceptions
{
    [Serializable]
    internal class RoleAlreadyExistException : AlreadyExistException
    {
        public RoleAlreadyExistException()
        {
        }

        public RoleAlreadyExistException(string message) : base(message)
        {
        }

        public RoleAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RoleAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}