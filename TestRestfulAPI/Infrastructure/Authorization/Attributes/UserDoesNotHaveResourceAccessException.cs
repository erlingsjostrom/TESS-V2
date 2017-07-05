using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.Infrastructure.Authorization.Attributes
{
    [Serializable]
    internal class UserDoesNotHaveResourceAccessException : UnauthorizedAccessException
    {
        private object p;

        public UserDoesNotHaveResourceAccessException()
        {
        }

        public UserDoesNotHaveResourceAccessException(string message) : base(message)
        {
        }

        public UserDoesNotHaveResourceAccessException(object p)
        {
            this.p = p;
        }

        public UserDoesNotHaveResourceAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserDoesNotHaveResourceAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}