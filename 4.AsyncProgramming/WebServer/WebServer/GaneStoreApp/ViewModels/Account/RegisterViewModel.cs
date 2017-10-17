using System;
using System.ComponentModel.DataAnnotations;
using WebServer.GaneStoreApp.Common;
using WebServer.GaneStoreApp.Utilities;

namespace WebServer.GaneStoreApp.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name ="E-mail")]
        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLenght,ErrorMessage = ValidationConstants.InvalidMaxLenghtErrorMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [MinLength(ValidationConstants.Account.NameMinLenght,ErrorMessage = ValidationConstants.InvalidMinLenghtErrorMessage)]
        [MaxLength(ValidationConstants.Account.NameMaxLenght,ErrorMessage = ValidationConstants.InvalidMaxLenghtErrorMessage)]
        public string FullName { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLenght,ErrorMessage = ValidationConstants.InvalidMinLenghtErrorMessage)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLenght,ErrorMessage = ValidationConstants.InvalidMaxLenghtErrorMessage)]
        [Password]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
