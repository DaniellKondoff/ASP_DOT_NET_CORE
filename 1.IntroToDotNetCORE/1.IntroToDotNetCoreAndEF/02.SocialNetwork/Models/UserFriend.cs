using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _02.SocialNetwork.Models
{
    public class UserFriend
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("FriendId")]
        public int FriendId { get; set; }
        public User Friend { get; set; }
    }
}
