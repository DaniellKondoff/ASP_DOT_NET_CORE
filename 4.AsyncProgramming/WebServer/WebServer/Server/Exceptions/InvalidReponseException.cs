using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Exceptions
{
    public class InvalidReponseException : Exception
    {
        public InvalidReponseException(string message)
            :base(message)
        {

        }
    }
}
