using System;
using System.Collections.Generic;
using System.Text;

namespace _02.SocialNetwork.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string Path { get; set; }

        public List<AlbumsPictures> Albums { get; set; }

    }
}
