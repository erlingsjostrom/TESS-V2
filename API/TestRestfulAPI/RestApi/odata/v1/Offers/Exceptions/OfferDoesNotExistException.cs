using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Offers.Exceptions
{
    [Serializable]
    internal class OfferDoesNotExistException : DoesNotExistException
    {
        public OfferDoesNotExistException()
        {
        }

        public OfferDoesNotExistException(string message) : base(message)
        {
        }

        public OfferDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OfferDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}