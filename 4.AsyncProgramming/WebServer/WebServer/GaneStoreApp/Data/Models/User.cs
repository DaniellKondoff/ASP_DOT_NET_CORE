using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebServer.GaneStoreApp.Common;

namespace WebServer.GaneStoreApp.Data.Models
{
    public class User
    {
        
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.NameMinLenght)]
        [MaxLength(ValidationConstants.Account.NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLenght)]
        [MaxLength(ValidationConstants.Account.PasswordMaxLenght)]
        public string Password { get; set; }

        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLenght)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
