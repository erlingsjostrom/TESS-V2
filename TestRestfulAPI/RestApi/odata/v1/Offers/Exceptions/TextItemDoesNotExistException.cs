using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Exceptions
{
    [Serializable]
    internal class TextItemDoesNotExistException : DoesNotExistException
    {
        public TextItemDoesNotExistException()
        {
        }

        public TextItemDoesNotExistException(string message) : base(message)
        {
        }

        public TextItemDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TextItemDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}