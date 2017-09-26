using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _02.SocialNetwork.Models
{
    public partial class User
    {
        private string password;
        private string email;

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }

        public string Password
        {
            get => this.password;
            set
            {
                if (!this.CheckLowerLetter(value) || !this.CheckUpperLetter(value) || !this.CheckDigit(value) || !this.CheckSpecialSymbol(value))
                {
                    throw new ArgumentException("The password is not allowed");
                }
                this.password = value;
            }
        }

        public string Email
        {
            get => this.email;
            set
            {
                if (!this.CheckEmailValidation(value))
                {
                    throw new ArgumentException("Error! Email is not in valid format");
                }
                this.email = value;
            }
        }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserFriend> Friends { get; set; } = new List<UserFriend>();
        public ICollection<UserFriend> Users { get; set; } = new List<UserFriend>();

        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
