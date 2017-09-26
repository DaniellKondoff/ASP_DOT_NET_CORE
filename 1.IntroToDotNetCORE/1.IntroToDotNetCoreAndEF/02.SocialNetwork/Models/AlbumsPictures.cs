using System;
using System.Collections.Generic;
using System.Text;

namespace _02.SocialNetwork.Models
{
    public class AlbumsPictures
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int  PictureId { get; set; }
        public Picture Picture { get; set; }

    }
}
