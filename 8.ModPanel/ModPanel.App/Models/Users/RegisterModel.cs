using ModPanel.App.Data.Models;
using ModPanel.App.Infrastructure.Validation.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Models.Users
{
    public class RegisterModel
    {
        [Email]
        public string Email { get; set; }

        [Required]
        [Password]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        public int Position { get; set; }
    }
}
