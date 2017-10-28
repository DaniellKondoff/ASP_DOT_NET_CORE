using ModPanel.App.Data.Models;
using ModPanel.App.Models.Posts;
using ModPanel.App.Services;
using ModPanel.App.Services.Contracts;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Controllers
{
    public class PostsController : BaseController
    {
        private const string CreateError = "<p>Invalid Post</p>";
        private readonly IPostService posts;

        public PostsController()
        {
            this.posts = new PostService();
        }

        public IActionResult Create()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(PostModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToLogin();
            }

            if (!this.IsValidModel(model) || !char.IsUpper(model.Title[0]))
            {
                this.ShowError(CreateError);
                return this.View();
            }

            this.posts.Create(model.Title, model.Content, this.Profile.Id);

            if (this.IsAdmin)
            {
                this.Log(LogType.CreatePost, model.Title);
            }

            return this.RedirectToHome();
        }
    }
}
