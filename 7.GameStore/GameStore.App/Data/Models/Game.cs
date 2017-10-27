using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameStore.App.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Title { get; set; }

        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public double Size { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string VideoId { get; set; }


        public string Thumbnail { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<Order> Users { get; set; } = new List<Order>();
    }
}
