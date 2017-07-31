using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Exceptions
{
    [Serializable]
    internal class ContentDoesNotExistException : DoesNotExistException
    {
        public ContentDoesNotExistException()
        {
        }

        public ContentDoesNotExistException(string message) : base(message)
        {
        }

        public ContentDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ContentDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}