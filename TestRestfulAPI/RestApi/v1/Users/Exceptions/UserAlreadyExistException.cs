using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.v1.Users.Exceptions
{
    [Serializable]
    internal class UserAlreadyExistException : AlreadyExistException
    {
        public UserAlreadyExistException()
        {
        }

        public UserAlreadyExistException(string message) : base(message)
        {
        }

        public UserAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}