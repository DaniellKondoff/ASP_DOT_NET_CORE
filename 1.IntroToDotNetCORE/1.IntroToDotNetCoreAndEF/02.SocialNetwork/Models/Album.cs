using System;
using System.Collections.Generic;
using System.Text;

namespace _02.SocialNetwork.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public List<AlbumsPictures> Pictures { get; set; } = new List<AlbumsPictures>();

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
