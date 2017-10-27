using GameStore.App.Services;
using GameStore.App.Services.Contracts;
using SimpleMvc.Framework.Contracts;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SimpleMvc.Framework.Attributes.Methods;
using GameStore.App.Models.Games;

namespace GameStore.App.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IGameService gameService;
        public const string GameError = "<p>Invalid Attempt to add Game</p>";

        public AdminController()
        {
            this.gameService = new GameService();
        }

        public IActionResult AllGames()
        {
            if (!this.IsAdmin)
            {
                return this.Redirect("/");
            }

            var allGames = this.gameService
                .All()
                .Select(g => $@"
                    <tr> 
                        <td>{g.Id}</td>
                        <td>{g.Name}</td>
                        <td>{g.Price}&euro</td>
                        <td>{g.Size} GB</td>
                        <td><a class=""btn btn-warning btn-sm"" href=""/admin/editgame?id={g.Id}"">Edit</a>
                        <a class=""btn btn-danger btn-sm"" href=""/admin/deletegame?id={g.Id}"">Delete</a></td>
                    </tr>");

            var resultHtml = string.Join(string.Empty, allGames);

            this.ViewModel["games"] = resultHtml;

            return this.View();
        }

        public IActionResult AddGame()
        {
            if (!this.IsAdmin)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult AddGame(GameAdminModel model)
        {
            if (!this.Profile.IsAdmin)
            {
                return this.Redirect("/");
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError(GameError);
                return this.View();
            }

            this.gameService.Create(model.Title, model.Description, model.Thumbnail, model.Price, model.Size, model.VideoId, model.ReleaseDate);

            return this.Redirect("/admin/allgames");
        }

        public IActionResult EditGame(int id)
        {
            if (!this.Profile.IsAdmin)
            {
                return this.Redirect("/");
            }

            var game = this.gameService.GetById(id);

            if (game == null)
            {
                return this.NotFound();
            }

            this.ViewModel["title"] = game.Title;
            this.ViewModel["description"] = game.Description;
            this.ViewModel["thumbnail"] = game.Thumbnail;
            this.ViewModel["price"] = game.Price.ToString("F2");
            this.ViewModel["size"] = game.Size.ToString("F1");
            this.ViewModel["videoId"] = game.VideoId;
            this.ViewModel["release-date"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            return this.View();
        }

        [HttpPost]
        public IActionResult EditGame(int id, GameAdminModel model)
        {
            if (!this.Profile.IsAdmin)
            {
                return this.Redirect("/");
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError(GameError);
                return this.View();
            }

            this.gameService.Update(id,model.Title, model.Description, model.Thumbnail, model.Price, model.Size, model.VideoId, model.ReleaseDate);

            return this.Redirect("/admin/allgames");
        }

        public IActionResult DeleteGame(int id)
        {
            if (!this.Profile.IsAdmin)
            {
                return this.Redirect("/");
            }

            var game = this.gameService.GetById(id);

            if (game == null)
            {
                return this.NotFound();
            }

            this.ViewModel["title"] = game.Title;
            this.ViewModel["description"] = game.Description;
            this.ViewModel["thumbnail"] = game.Thumbnail;
            this.ViewModel["price"] = game.Price.ToString("F2");
            this.ViewModel["size"] = game.Size.ToString("F1");
            this.ViewModel["videoId"] = game.VideoId;
            this.ViewModel["release-date"] = game.ReleaseDate.ToString("yyyy-MM-dd");
            this.ViewModel["id"] = id.ToString();

            return this.View();
        }

        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            if (!this.Profile.IsAdmin)
            {
                return this.Redirect("/");
            }

            this.gameService.Delete(id);

            return this.Redirect("/admin/allgames");
        }
    }
}
