using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Articles.Exceptions
{
    [Serializable]
    internal class ArticleAlreadyExistException : AlreadyExistException
    {
        public ArticleAlreadyExistException()
        {
        }

        public ArticleAlreadyExistException(string message) : base(message)
        {
        }

        public ArticleAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArticleAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}