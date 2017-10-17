using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using WebServer.GaneStoreApp.Controllers;
using WebServer.GaneStoreApp.Data;
using WebServer.GaneStoreApp.ViewModels.Account;
using WebServer.GaneStoreApp.ViewModels.Admin;
using WebServer.Server.Contracts;
using WebServer.Server.Routing.Contracts;

namespace WebServer.GaneStoreApp
{
    public class GameStoreApplication : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new GameStoreDbContext())
            {
                db.Database.Migrate();
            }
        }
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.anonymousPaths.Add("/");
            appRouteConfig.anonymousPaths.Add("/account/register");
            appRouteConfig.anonymousPaths.Add("/account/login");

            appRouteConfig
                .Get("/account/register", req => new AccountController(req).Register());

            appRouteConfig
                .Post("account/register", req => new AccountController(req).Register(new RegisterViewModel{
                    Email = req.FormData["email"],
                    FullName = req.FormData["full-name"],
                    Password = req.FormData["password"],
                    ConfirmPassword = req.FormData["confirm-password"]
                }));

            appRouteConfig
                .Get("/account/login", req => new AccountController(req).Login());

            appRouteConfig
                .Post("/account/login", req => new AccountController(req).Login(new LoginViewModel
                {
                    Email = req.FormData["email"],
                    Password = req.FormData["password"]
                }));

            appRouteConfig
                .Get("/account/logout", req => new AccountController(req).Logout());

            appRouteConfig
                .Get("/", req => new HomeController(req).Index());

            appRouteConfig
                .Get("/admin/games/add", req => new AdminController(req).Add());

            appRouteConfig
               .Post("/admin/games/add", req => new AdminController(req).Add(new AddGameViewModel
               {
                   Title = req.FormData["title"],
                   Description = req.FormData["description"],
                   Image = req.FormData["thumbnail"],
                   Price = decimal.Parse(req.FormData["price"]),
                   Size = double.Parse(req.FormData["size"]),
                   VideoId = req.FormData["video-id"],
                   ReleaseDate = DateTime
                   .ParseExact(req.FormData["release-date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
               }));

            appRouteConfig
                .Get("/admin/games/list", req => new AdminController(req).List());

            appRouteConfig
                .Get("/admin/games/edit/{(?<id>[0-9]+)}", req => new AdminController(req).Edit());

            appRouteConfig
               .Post("/admin/games/edit/{(?<id>[0-9]+)}", req => new AdminController(req).Edit(new AddGameViewModel
               {
                   Title = req.FormData["title"],
                   Description = req.FormData["description"],
                   Image = req.FormData["thumbnail"],
                   Price = decimal.Parse(req.FormData["price"]),
                   Size = double.Parse(req.FormData["size"]),
                   VideoId = req.FormData["video-id"],
                   ReleaseDate = DateTime
                   .ParseExact(req.FormData["release-date"], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                
               }));

            appRouteConfig
                .Get("/admin/games/delete/{(?<id>[0-9]+)}", req => new AdminController(req).Delete());

            appRouteConfig
                .Post("/admin/games/delete/{(?<id>[0-9]+)}", req => new AdminController(req).Delete(req.UrlParameters["id"]));
        }
    }
}
