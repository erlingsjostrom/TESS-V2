﻿using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Customers.Exceptions
{
    [Serializable]
    internal class CustomerDoesNotExistException : DoesNotExistException
    {
        public CustomerDoesNotExistException()
        {
        }

        public CustomerDoesNotExistException(string message) : base(message)
        {
        }

        public CustomerDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}