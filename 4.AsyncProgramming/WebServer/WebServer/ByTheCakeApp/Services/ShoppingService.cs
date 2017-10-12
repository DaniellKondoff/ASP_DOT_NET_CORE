using System;
using System.Collections.Generic;
using System.Linq;
using WebServer.ByTheCakeApp.Data;
using WebServer.ByTheCakeApp.Data.Models;
using WebServer.ByTheCakeApp.Services.Contracts;
using WebServer.ByTheCakeApp.ViewModels.Shopping;

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

        public IEnumerable<OrdersDetailsViewModel> GetOrders(int userId)
        {
            using (var db = new CakeDbContext())
            {
               return db.Orders
                    .Where(o => o.UserId == userId)
                    .Select(o => new OrdersDetailsViewModel
                    {
                        Id = o.Id,
                        CreatedOn = o.CreationDate,
                        Sum = o.Products.Sum(p => p.Product.Price)
                    })
                    .OrderByDescending(o=>o.CreatedOn)
                    .ToList();
            }
        }

        public OrderDetailsByIdViewModel Find(int id)
        {
            using (var db = new CakeDbContext())
            {
                return db.Orders
                    .Where(o => o.Id == id)
                    .Select(o => new OrderDetailsByIdViewModel
                    {
                        Id = o.Id,
                        CreatedOn = o.CreationDate,
                        Products = o.Products
                        .Where(p=>p.OrderId ==o.Id)
                        .Select(p => new OrderProductsDetailsViewModel
                        {
                            Id = p.ProductId,
                            Name=p.Product.Name,
                            Price = p.Product.Price
                        })
                    })
                    .FirstOrDefault();
            }
        }
    }
}
