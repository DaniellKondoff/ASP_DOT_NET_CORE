using System.Linq;
using WebServer.ByTheCakeApp.Data;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class ShoppingController : ControllerBase
    {
        private readonly CakesData cakesData;

        public ShoppingController()
        {
            this.cakesData = new CakesData();
        }

        public IHttpResponse AddToCart(IHttpRequest request)
        {
            var idNumber = int.Parse(request.UrlParameters["id"]);

            var cake = this.cakesData.Find(idNumber);

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = request.Session.Get<ShoppingCard>(ShoppingCard.SessionKey);
            shoppingCart.Orders.Add(cake);

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

            if (!shoppingCart.Orders.Any())
            {
                this.ViewData["cartItems"] = "No items in you cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var items = shoppingCart.Orders.Select(i => $"<div>{i.Name} - ${i.Price:F2}</div> <br />");

                this.ViewData["cartItems"] = string.Join(string.Empty, items);

                var totalPrice = shoppingCart.Orders.Sum(i => i.Price);

                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest request)
        {
            request.Session.Get<ShoppingCard>(ShoppingCard.SessionKey).Orders.Clear();

            return this.FileViewResponse(@"shopping\finish-order");
        }
    }
}
