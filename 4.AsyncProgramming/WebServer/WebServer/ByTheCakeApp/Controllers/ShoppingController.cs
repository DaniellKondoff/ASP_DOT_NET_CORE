using System;
using System.Linq;
using WebServer.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.ByTheCakeApp.Services;
using WebServer.ByTheCakeApp.Services.Contracts;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class ShoppingController : BaseController
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IShoppingService shoppingService;

        public ShoppingController()
        {
            this.productService = new ProductService();
            this.userService = new UserService();
            this.shoppingService = new ShoppingService();
        }

        public IHttpResponse AddToCart(IHttpRequest request)
        {
            var id = int.Parse(request.UrlParameters["id"]);

            var productExists = this.productService.Exists(id);

            if (!productExists)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = request.Session.Get<ShoppingCard>(ShoppingCard.SessionKey);
            shoppingCart.ProductIds.Add(id);

            var redirectUrl = "/search";

            const string searchTermKey = "search";

            if (request.QueryParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={request.QueryParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCard>(ShoppingCard.SessionKey);

            if (!shoppingCart.ProductIds.Any())
            {
                this.ViewData["cartItems"] = "No items in you cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var productsInCart = this.productService
                    .FindProductsInCart(shoppingCart.ProductIds);

                var items = productsInCart.Select(pr => $"<div>{pr.Name} - ${pr.Price:F2}</div> <br />");

                this.ViewData["cartItems"] = string.Join(string.Empty, items);

                var totalPrice = productsInCart.Sum(pr => pr.Price);

                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest request)
        {
            var username = request.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = request.Session.Get<ShoppingCard>(ShoppingCard.SessionKey);

            var userId = this.userService.GetUserId(username);

            if (userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist");
            }

            var productIds = shoppingCart.ProductIds;
            if (!productIds.Any())
            {
                return new RedirectResponse("/");
            }

            this.shoppingService.CreateOrder(userId.Value,productIds);

            shoppingCart.ProductIds.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }

        public IHttpResponse GetOrdersDetails(IHttpRequest request)
        {
            var username = request.Session.Get<string>(SessionStore.CurrentUserKey);

            var userId = this.userService.GetUserId(username);
            if (userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist");
            }

            var ordersDetails = this.shoppingService.GetOrders(userId.Value);
            if (!ordersDetails.Any())
            {
                return new RedirectResponse("/");
            }

            var ordersDetailsToHtml = ordersDetails
                .Select(o => $@"<tr><td><a href=""/order/{o.Id}"">{o.Id}</a></td><td>{o.CreatedOn.ToShortDateString()}</td><td>${o.Sum:F2}</td></tr>");

            var resultToHtml = string.Join(Environment.NewLine, ordersDetailsToHtml);

            this.ViewData["contentTable"] = resultToHtml;

            return this.FileViewResponse(@"shopping\details");
        }

        public IHttpResponse GetOrderDetailsById(int orderId)
        {
            var order = this.shoppingService.Find(orderId);

            if (order == null)
            {
                return new NotFoundResponse();
            }

            var allProductsbyOrder = order.Products;

            var productsToHtml = allProductsbyOrder
                .Select(p => $@"<tr><td><a href=""/cakes/{p.Id}"">{p.Name}</a></td><td>{p.Price:F2}</td></tr>");

            var resultForHtml = string.Join(Environment.NewLine, productsToHtml);

            this.ViewData["orderId"] = order.Id.ToString();
            this.ViewData["contentTable"] = resultForHtml;
            this.ViewData["createdDate"] = order.CreatedOn.ToShortDateString();
            return this.FileViewResponse(@"shopping/order-details");
        }
    }
}
