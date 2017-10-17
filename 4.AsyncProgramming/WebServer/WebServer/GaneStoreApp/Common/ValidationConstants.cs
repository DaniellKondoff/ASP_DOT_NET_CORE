using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GaneStoreApp.Common
{
    public class ValidationConstants
    {
        public const string InvalidMinLenghtErrorMessage = "{0} must be at least {1} symbols.";
        public const string InvalidMaxLenghtErrorMessage = "{0} cannot be more than {1} symbols.";
        public const string ExactLenghtErrorMessage = "{0} mus be exactly {1} symbols.";

        public class Account
        {
            public const int EmailMaxLenght = 30;
            public const int NameMinLenght = 2;
            public const int NameMaxLenght = 30;
            public const int PasswordMinLenght = 6;
            public const int PasswordMaxLenght = 50;
        }

        public class Game
        {
            public const int TitleMinLenght = 3;
            public const int TitleMaxLenght = 100;
            public const int VideoLenght = 11;
            public const int DexcriptionLenght = 20;
        }
    }
}
