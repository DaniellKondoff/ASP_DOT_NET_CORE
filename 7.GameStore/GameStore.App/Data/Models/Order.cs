using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Data.Models
{
    public class Order
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }


    }
}
