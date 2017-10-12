using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.ByTheCakeApp.ViewModels.Shopping
{
    public class OrderDetailsByIdViewModel
    {
        public int Id { get; set; }

        public IEnumerable<OrderProductsDetailsViewModel> Products { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
