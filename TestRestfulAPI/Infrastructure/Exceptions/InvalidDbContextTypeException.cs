using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.Infrastructure.Helpers
{
    [Serializable]
    internal class InvalidDbContextTypeException : Exception
    {
        public InvalidDbContextTypeException()
        {
        }

        public InvalidDbContextTypeException(string message) : base(message)
        {
        }

        public InvalidDbContextTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDbContextTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}