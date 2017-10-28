using SimpleMvc.Framework.Attributes.Validation;

namespace ModPanel.App.Infrastructure.Validation.Users
{
    public class RequiredAttribute : PropertyValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return new System.ComponentModel.DataAnnotations.RequiredAttribute().IsValid(value);
        }
    }
}
