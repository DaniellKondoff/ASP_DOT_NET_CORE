using System;
using System.Collections.Generic;
using System.Text;
using WebServer.ByTheCakeApp.ViewModels.Products;

namespace WebServer.ByTheCakeApp.Services.Contracts
{
    public interface IProductService
    {
        void Create(string name, decimal price, string imageUrl);

        IEnumerable<ProductListingViewModel> All(string searchTerm = null);

        ProductDetailsViewModel Find(int id);

        bool Exists(int id);

        IEnumerable<ProductInCartViewModel> FindProductsInCart(IEnumerable<int> ids);
    }
}
