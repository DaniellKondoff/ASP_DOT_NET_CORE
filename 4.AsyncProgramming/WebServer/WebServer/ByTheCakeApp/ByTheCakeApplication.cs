using WebServer.ByTheCakeApp.Controllers;
using WebServer.Server.Contracts;
using WebServer.Server.Routing.Contracts;

namespace WebServer.ByTheCakeApp
{
    public class ByTheCakeApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                 .Get("/", request => new HomeController().Index());

            appRouteConfig
                .Get("/about", request => new HomeController().About());

            appRouteConfig
                .Get("/add", request => new CakeController().Add());

            appRouteConfig
                .Post("/add", request => new CakeController().Add(request.FormData["name"],request.FormData["price"]));

            appRouteConfig
                .Get("/search", request => new CakeController().Search(request));

            appRouteConfig
                .Get("/calc", request => new CalculatorController().Calculate());

            appRouteConfig
                .Post("/calc", request => new CalculatorController()
                 .Calculate(request.FormData["number1"], request.FormData["method"], request.FormData["number2"]));

            appRouteConfig
                .Get("/login", request => new AccountController().Login());

            appRouteConfig
                .Post("/login", request => new AccountController()
                .Login(request));

            appRouteConfig
                .Post("/logout", request => new AccountController().Logout(request));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", request => new ShoppingController().AddToCart(request));

            appRouteConfig
                .Get("/cart", request => new ShoppingController().ShowCart(request));

            appRouteConfig
                .Post("/shopping/finish-order", request => new ShoppingController().FinishOrder(request));

        }
    }
}
