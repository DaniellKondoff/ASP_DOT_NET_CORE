using ModPanel.App.Infrastructure.Validation.Posts;
using ModPanel.App.Infrastructure.Validation.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Models.Posts
{
    public class PostModel
    {
        [Required]
        [Title]
        public string Title { get; set; }

        [Required]
        [Content]
        public string Content { get; set; }
    }
}
