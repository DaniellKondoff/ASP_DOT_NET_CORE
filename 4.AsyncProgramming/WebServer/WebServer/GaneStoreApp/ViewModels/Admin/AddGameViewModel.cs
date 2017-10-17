using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebServer.GaneStoreApp.Common;

namespace WebServer.GaneStoreApp.ViewModels.Admin
{
    public class AddGameViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLenght,
            ErrorMessage =ValidationConstants.InvalidMinLenghtErrorMessage)]
        [MaxLength(ValidationConstants.Game.TitleMaxLenght,
            ErrorMessage =ValidationConstants.InvalidMaxLenghtErrorMessage)]
        public string Title { get; set; }

        [Display(Name ="YouTube Video URL")]
        [Required]
        [MinLength(ValidationConstants.Game.TitleMinLenght,
            ErrorMessage =ValidationConstants.ExactLenghtErrorMessage)]
        [MaxLength(ValidationConstants.Game.TitleMaxLenght,
            ErrorMessage = ValidationConstants.ExactLenghtErrorMessage)]
        public string VideoId { get; set; }

        [Required]
        public string Image { get; set; }

        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(ValidationConstants.Game.DexcriptionLenght,
            ErrorMessage =ValidationConstants.ExactLenghtErrorMessage)]
        public string Description { get; set; }

        [Display(Name ="Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
    }
}
