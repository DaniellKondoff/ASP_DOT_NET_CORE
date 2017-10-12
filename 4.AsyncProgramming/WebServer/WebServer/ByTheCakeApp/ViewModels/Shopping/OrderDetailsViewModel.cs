using System;

namespace WebServer.ByTheCakeApp.ViewModels.Shopping
{
    public class OrdersDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Sum { get; set; }
    }
}
