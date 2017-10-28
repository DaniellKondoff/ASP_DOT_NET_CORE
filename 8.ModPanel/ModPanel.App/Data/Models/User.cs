using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModPanel.App.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsApproved { get; set; }

        public PositionType Position { get; set; }

        public List<Post> Post { get; set; } = new List<Post>();
    }
}
