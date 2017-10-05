using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string InvalidRequestMessage = "Request is not Valid.";

        public static void ThrowFromInvalidRequest()
        {
            throw new BadRequestException(InvalidRequestMessage);
        }

        public BadRequestException(string message)
            :base(message)
        {
          
        }
    }
}
