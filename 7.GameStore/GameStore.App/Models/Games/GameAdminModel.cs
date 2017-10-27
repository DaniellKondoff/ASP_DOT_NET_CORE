using GameStore.App.Infrastructure.Validation;
using SimpleMvc.Framework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Models.Games
{
    public class GameAdminModel
    {
        [Required]
        [Title]
        public string Title { get; set; }

        [Required]
        [Description]
        public string Description { get; set; }

        [Thumbnail]
        public string Thumbnail { get; set; }

        [NumberRange(0,double.MaxValue)]
        public decimal Price { get; set; }

        [NumberRange(0, double.MaxValue)]
        public double Size { get; set; }

        [Required]
        [Video]
        public string VideoId { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}
