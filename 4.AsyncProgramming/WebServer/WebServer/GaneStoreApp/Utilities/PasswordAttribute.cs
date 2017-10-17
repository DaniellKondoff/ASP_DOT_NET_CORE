using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebServer.GaneStoreApp.Utilities
{
    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage = "Password should be at least 6 symbols long, should contains 1 uppercase, 1 lowercase and 1 digit";
        }
        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
            {
                return true;
            }

            return password.Any(s => char.IsUpper(s)) 
                && password.Any(s => char.IsLower(s)) 
                && password.Any(s => char.IsDigit(s));
        }
    }
}
