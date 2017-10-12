using System.Collections.Generic;
using WebServer.ByTheCakeApp.ViewModels.Shopping;

namespace WebServer.ByTheCakeApp.Services.Contracts
{
    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> productIds);

        IEnumerable<OrdersDetailsViewModel> GetOrders(int userId);

        OrderDetailsByIdViewModel Find(int id);
    }
}
