using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.Infrastructure.Authorization
{
    [Serializable]
    internal class UserDoesNotHaveRequiredRolesException : UnauthorizedAccessException
    {
        public UserDoesNotHaveRequiredRolesException()
        {
        }

        public UserDoesNotHaveRequiredRolesException(string message) : base(message)
        {
        }

        public UserDoesNotHaveRequiredRolesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserDoesNotHaveRequiredRolesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}