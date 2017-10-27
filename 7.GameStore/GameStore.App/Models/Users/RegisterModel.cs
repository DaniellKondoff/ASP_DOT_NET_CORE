using GameStore.App.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Models.Users
{
    public class RegisterModel
    {
        [Email]
        public string Email { get; set; }

        public string Name { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

    }
}
