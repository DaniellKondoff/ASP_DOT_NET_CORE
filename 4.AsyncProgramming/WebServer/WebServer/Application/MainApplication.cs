using System;
using WebServer.Application.Controllers;
using WebServer.Server.Contracts;
using WebServer.Server.Handlers;
using WebServer.Server.Routing.Contracts;

namespace WebServer.Application
{
    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", request => new HomeController().Index());

            appRouteConfig
                .AddRoute("/about", new GetHandler(request => new HomeController().About()));
        }
    }
}
