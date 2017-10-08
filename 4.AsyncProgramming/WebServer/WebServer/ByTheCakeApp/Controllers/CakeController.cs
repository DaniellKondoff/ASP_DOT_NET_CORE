using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebServer.ByTheCakeApp.Data;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class CakeController : ControllerBase
    {
        private readonly CakesData cakesData;

        public CakeController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse Add()
        {
            this.ViewData["display"] = "none";
            return this.FileViewResponse(@"cake\add");
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };


            this.cakesData.Add(name, price);

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(@"Cake\add");
        }

        public IHttpResponse Search(IHttpRequest request)
        {
            const string searchTermKey = "search";

            var urlParameters = request.UrlParameters;

            var results = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                this.ViewData["searchTerm"] = searchTerm;

                var savedCakesDivs = this.cakesData.All()
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $@"<div>{c.Name} - ${c.Price} <a href=""/shopping/add/{c.Id}?search={searchTerm}"">Order</a></div>");

                results = "No Cakes found";

                if (savedCakesDivs.Any())
                {
                    results = string.Join(Environment.NewLine, savedCakesDivs);

                }
            }


            this.ViewData["showCart"] = "none";
            this.ViewData["results"] = results;

            var shoppingCart = request.Session.Get<ShoppingCard>(ShoppingCard.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count();
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";

            }
            return this.FileViewResponse(@"Cake\search");
        }

       
    }
}
