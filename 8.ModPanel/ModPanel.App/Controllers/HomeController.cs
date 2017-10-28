using ModPanel.App.Infrastructure;
using ModPanel.App.Services;
using ModPanel.App.Services.Contracts;
using SimpleMvc.Framework.Contracts;
using SimpleMvc.Framework.Controllers;
using System.Linq;

namespace ModPanel.App.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPostService posts;
        private readonly ILogService logService;


        public HomeController()
        {
            this.posts = new PostService();
            this.logService = new LogService();
        }

        public IActionResult Index()
        {
            this.ViewModel["guestDisplay"] = "flex";
            this.ViewModel["auth"] = "none";
            this.ViewModel["admin"] = "none";

            if (this.User.IsAuthenticated)
            {
                this.ViewModel["guestDisplay"] = "none";
                this.ViewModel["auth"] = "flex";

                string search = null;
                if (this.Request.UrlParameters.ContainsKey("search"))
                {
                    search = this.Request.UrlParameters["search"];
                }

                var postsData = this.posts.AllWithDetails(search);


                var postsCards = postsData
                    .Select(p => $@"<div class=""card border-primary mb-3"">
                    <div class=""card-body text-primary"">
                        <h4 class=""card-title"">{p.Title}</h4>
                        <p class=""card-text"">
                            {p.Content}
                        </p>
                    </div>
                    <div class=""card-footer bg-transparent text-right"">
                        <span class=""text-muted"">
                            Created on 12.10.2017 by
                            <em>
                                <strong>{p.CreatedBy}</strong>
                            </em>
                        </span>
                    </div>
                </div>");

                this.ViewModel["posts"] = string.Join(string.Empty, postsCards);
                if (this.IsAdmin)
                {
                    this.ViewModel["admin"] = "flex";
                    this.ViewModel["auth"] = "none";

                    var logs = this.logService
                                .All()
                                .Select(l => l.ToHtml());

                    this.ViewModel["logs"] = string.Join(string.Empty, logs);
                }
            }
            return this.View();
        }
    }
}
