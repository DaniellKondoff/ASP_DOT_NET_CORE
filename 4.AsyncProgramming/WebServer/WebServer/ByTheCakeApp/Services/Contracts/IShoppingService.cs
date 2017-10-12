using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.ByTheCakeApp.Services.Contracts
{
    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<int> productIds);
    }
}
