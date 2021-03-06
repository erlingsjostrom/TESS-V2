﻿using System;
using System.Runtime.Serialization;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.RestApi.odata.v1.Customers.Exceptions
{
    [Serializable]
    internal class CustomerAlreadyExistException : AlreadyExistException
    {
        public CustomerAlreadyExistException()
        {
        }

        public CustomerAlreadyExistException(string message) : base(message)
        {
        }

        public CustomerAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}