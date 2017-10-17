using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GaneStoreApp.Services;
using WebServer.GaneStoreApp.Services.Contracts;
using WebServer.GaneStoreApp.ViewModels.Admin;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.GaneStoreApp.Controllers
{
    public class AdminController : BaseController
    {
        private const string AddGameView = @"admin/add-game";
        private const string ListGameView = @"admin/list-games";
        private const string EditGameView = @"admin/edit-game";
        private const string DeleteGameView = @"admin/delete-game";

        private readonly IGameService gameService;

        public AdminController(IHttpRequest request) 
            : base(request)
        {
            gameService = new GameService();
        }

        public IHttpResponse Add()
        {
            if (this.Authentication.IsAdmin)
            {
                return this.FileViewResponse(AddGameView);
            }
            else
            {
                return this.RedirectResponse(HomePath);
            }

        }

        public IHttpResponse Add(AddGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }
            if (!this.ValidateModel(model))
            {
                return this.Add();
            }


            this.gameService.Create(
                model.Title,
                model.Description,
                model.Image,
                model.Price,
                model.Size,
                model.VideoId,
                model.ReleaseDate.Value
                );

            return this.RedirectResponse("/admin/games/list");
        }

        public IHttpResponse List()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var games = this.gameService
                .All()
                .Select(g => 
                $@"<tr><td>{g.Id}</td><td>{g.Name}</td><td>{g.Size:F2} GB</td><td>{g.Price:F2} &euro;</td>
 <td><a class=""btn btn-warning"" href=""/admin/games/edit/{g.Id}"">Edit</a>
<a class=""btn btn-danger"" href=""/admin/games/delete/{g.Id}"">Delete</a></td></tr>");

            var gamesAsHtml = string.Join(Environment.NewLine, games);
            this.ViewData["games"] = gamesAsHtml;

            return this.FileViewResponse(ListGameView);
        }

        public IHttpResponse Edit()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var gameId = int.Parse(this.Request.UrlParameters["id"]);

            var game = this.gameService.Find(gameId);

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["image"] = game.Image;
            this.ViewData["price"] = game.Price.ToString();
            this.ViewData["size"] = game.Size.ToString();
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["releaseDate"] = game.ReleaseDate.Value.ToShortDateString();

            return this.FileViewResponse(EditGameView);
        }

        public IHttpResponse Edit(AddGameViewModel model)
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }
            if (!this.ValidateModel(model))
            {
                return this.Edit();
            }

            this.gameService.Edit(
               int.Parse(this.Request.UrlParameters["id"]),
               model.Title,
               model.Description,
               model.Image,
               model.Price,
               model.Size,
               model.VideoId,
               model.ReleaseDate.Value
               );

            return this.RedirectResponse("/admin/games/list");
        }

        public IHttpResponse Delete()
        {
            if (!this.Authentication.IsAdmin)
            {
                return this.RedirectResponse(HomePath);
            }

            var gameId = int.Parse(this.Request.UrlParameters["id"]);

            var game = this.gameService.Find(gameId);

            this.ViewData["title"] = game.Title;
            this.ViewData["description"] = game.Description;
            this.ViewData["image"] = game.Image;
            this.ViewData["price"] = game.Price.ToString();
            this.ViewData["size"] = game.Size.ToString();
            this.ViewData["videoId"] = game.VideoId;
            this.ViewData["releaseDate"] = game.ReleaseDate.Value.ToShortDateString();

            return this.FileViewResponse(DeleteGameView);
        }

        public IHttpResponse Delete(string id)
        {
            var gameId = int.Parse(id);

            bool success = this.gameService.Delete(gameId);

            if (!success)
            {
                this.ShowError("There is no such Game");

                return this.FileViewResponse(ListGameView);
            }
            else
            {
                return this.RedirectResponse("/admin/games/list");
            }
        }
    }
}
