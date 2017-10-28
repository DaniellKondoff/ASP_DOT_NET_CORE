using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModPanel.App.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }


    }
}
