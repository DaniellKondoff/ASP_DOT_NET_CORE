using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.ByTheCakeApp.ViewModels.Account
{
    public class RegisterUserViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
