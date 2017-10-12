using System;
using System.Linq;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.ByTheCakeApp.Services;
using WebServer.ByTheCakeApp.Services.Contracts;
using WebServer.ByTheCakeApp.ViewModels.Products;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class ProductsController : ControllerBase
    {
        private const string AddView = @"products\add";
        private readonly IProductService productService;

        public ProductsController()
        {
            this.productService = new ProductService();
        }

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";
            return this.FileViewResponse(AddView);
        }

        public IHttpResponse Add(AddProductViewModel model)
        {
            if (model.Name.Length < 3 || model.Name.Length > 30 || model.ImageUrl.Length <3 || model.ImageUrl.Length > 2000)
            {
                this.AddError("Invalid Product");

                return this.FileViewResponse(AddView);
            }

            this.productService.Create(model.Name, model.Price, model.ImageUrl);

            this.ViewData["name"] = model.Name;
            this.ViewData["price"] = model.Price.ToString();
            this.ViewData["imageUrl"] = model.ImageUrl;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(AddView);
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            const string searchTermKey = "search";

            var urlParameters = request.UrlParameters;

            var results = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            var searchTerm = urlParameters.ContainsKey(searchTermKey)
                ? urlParameters[searchTermKey]
                : null;

            var result = this.productService.All(searchTerm);

            if (!result.Any())
            {
                this.ViewData["results"] = "No Cakes found";
            }
            else
            {
                var allProducts = result
                    .Select(c => $@"<div><a href=""/cakes/{c.Id}"">{c.Name}</a> - ${c.Price} <a href=""/shopping/add/{c.Id}?search={searchTerm}"">Order</a></div>");
                var allproductsAsString = string.Join(Environment.NewLine, allProducts);

                this.ViewData["results"] = allproductsAsString;
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = request.Session.Get<ShoppingCard>(ShoppingCard.SessionKey);

            if (shoppingCart.ProductIds.Any())
            {
                var totalProducts = shoppingCart.ProductIds.Count();
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";

            }
            return this.FileViewResponse(@"products\search");
        }

        public IHttpResponse Details(int id)
        {

            var product = this.productService.Find(id);

            if (product == null)
            {
                return new NotFoundResponse();
            }

            this.ViewData["name"] = product.Name;
            this.ViewData["price"] = product.Price.ToString("F2");
            this.ViewData["imageUrl"] = product.ImageUrl;

            return this.FileViewResponse(@"products\details");
        }
       
    }
}
