namespace WebServer.ByTheCakeApp.Services
{
    using System;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Services.Contracts;
    using ViewModels.Products;
    using System.Linq;

    public class ProductService : IProductService
    {
        public void Create(string name, decimal price, string imageUrl)
        {
            using (var db = new CakeDbContext())
            {
                var product = new Product
                {
                    Name = name,
                    Price = price,
                    ImageUrl = imageUrl
                };

                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductListingViewModel> All(string searchTerm = null)
        {
            using (var db = new CakeDbContext())
            {
                var resultsQuery = db.Products.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    resultsQuery = resultsQuery
                        .Where(pr => pr.Name.ToLower().Contains(searchTerm.ToLower()));
                }

                return resultsQuery
                    .Select(pr => new ProductListingViewModel
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        Price = pr.Price
                    })
                    .ToList();
            }
        }

        public ProductDetailsViewModel Find(int id)
        {
            using (var db = new CakeDbContext())
            {
                return db
                    .Products
                    .Where(p => p.Id == id)
                    .Select(p => new ProductDetailsViewModel
                    {
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        Price = p.Price
                    })
                    .FirstOrDefault();
                    
            }
        }

        public bool Exists(int id)
        {
            using (var db = new CakeDbContext())
            {
               return db.Products.Any(pr => pr.Id == id);
            }
        }

        public IEnumerable<ProductInCartViewModel> FindProductsInCart(IEnumerable<int> ids)
        {
            using (var db = new CakeDbContext())
            {
                return db.Products
                    .Where(pr => ids.Contains(pr.Id))
                    .Select(pr => new ProductInCartViewModel
                    {
                        Name = pr.Name,
                        Price = pr.Price
                    })
                    .ToList();
            }
        }
    }
}
