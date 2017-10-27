using SimpleMvc.Framework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.App.Infrastructure.Validation
{
    public class PasswordAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;

            if (password == null)
            {
                return true;
            }

            return password.Any(s => char.IsDigit(s))
                && password.Any(s => char.IsUpper(s))
                && password.Any(s => char.IsLower(s))
                && password.Length >= 6;
        }
    }
}
