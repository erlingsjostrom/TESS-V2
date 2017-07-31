using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Contents.Exceptions
{
    [Serializable]
    internal class TemplateDoesNotExistException : DoesNotExistException
    {
        public TemplateDoesNotExistException()
        {
        }

        public TemplateDoesNotExistException(string message) : base(message)
        {
        }

        public TemplateDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TemplateDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}