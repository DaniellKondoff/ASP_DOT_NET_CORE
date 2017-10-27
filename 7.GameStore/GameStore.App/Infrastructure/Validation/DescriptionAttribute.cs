using SimpleMvc.Framework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Infrastructure.Validation
{
    public class DescriptionAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string description = value as string;
            if (description == null)
            {
                return true;
            }

            return description.Length >= 20;
        }
    }
}
