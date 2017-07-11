using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.Articles.Exceptions
{
    [Serializable]
    internal class ArticleDoesNotExistException : DoesNotExistException
    {
        public ArticleDoesNotExistException()
        {
        }

        public ArticleDoesNotExistException(string message) : base(message)
        {
        }

        public ArticleDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArticleDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}