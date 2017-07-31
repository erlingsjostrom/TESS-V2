using System;
using System.Runtime.Serialization;

namespace TestRestfulAPI.Infrastructure.Helpers
{
    [Serializable]
    internal class InvalidDbConnectionFactoryInput : Exception
    {
        public InvalidDbConnectionFactoryInput()
        {
        }

        public InvalidDbConnectionFactoryInput(string message) : base(message)
        {
        }

        public InvalidDbConnectionFactoryInput(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDbConnectionFactoryInput(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}