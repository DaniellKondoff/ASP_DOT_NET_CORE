using SimpleMvc.Framework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Infrastructure.Validation
{
    public class ThumbnailAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string thumbnail = value as string;
            if (thumbnail == null)
            {
                return true;
            }

            return thumbnail.StartsWith("https://") || thumbnail.StartsWith("http://");
        }
    }
}
