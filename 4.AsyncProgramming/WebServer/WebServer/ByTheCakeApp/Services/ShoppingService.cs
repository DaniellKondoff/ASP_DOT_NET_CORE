using System;
using System.Collections.Generic;
using System.Linq;
using WebServer.ByTheCakeApp.Data;
using WebServer.ByTheCakeApp.Data.Models;
using WebServer.ByTheCakeApp.Services.Contracts;

namespace WebServer.ByTheCakeApp.Services
{
    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId,IEnumerable<int> productIds)
        {
            using (var db = new CakeDbContext())
            {
                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds
                        .Select(id => new OrderProduct
                        {
                            ProductId = id
                        })
                        .ToList()
                };

                db.Add(order);
                db.SaveChanges();
            }
        }
    }
}
