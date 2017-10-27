using SimpleMvc.Framework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Infrastructure.Validation
{
    public class EmailAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var email = value as string;

            if (email==null)
            {
                return true;
            }

            return email.Contains(".") && email.Contains("@");
        }
    }
}
