using SimpleMvc.Framework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Infrastructure.Validation.Posts
{
    public class ContentAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string content = value as string;

            if (content == null)
            {
                return true;
            }

            return content.Length >= 10;
        }
    }
}
