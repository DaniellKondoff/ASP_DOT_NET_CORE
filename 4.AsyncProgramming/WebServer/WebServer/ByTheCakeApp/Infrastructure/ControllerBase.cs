using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using WebServer.ByTheCakeApp.Views.Home;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.ByTheCakeApp.Infrastructure
{
    public abstract class ControllerBase
    {
        public const string DefaultPath = @"ByTheCakeApp\Resource\{0}.html";
        public const string ContentPlaceholder = @"{{{content}}}";

        protected IDictionary<string,string> ViewData { get; private set; }

        protected ControllerBase()
        {
            this.ViewData = new Dictionary<string, string>()
            {
                ["authDisplay"] = "block",
                ["showError"] = "none"
        };
        }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var resultHtml = ProccessFileHtml(fileName);

            if ( this.ViewData.Values.Any())
            {
                foreach (var value in this.ViewData)
                {
                    resultHtml = resultHtml.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.OK, new FileView(resultHtml));
        }

        private string ProccessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));
            var fileHtml = File.ReadAllText(string.Format(DefaultPath, fileName));

            var resultHtml = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return resultHtml;
        }

        protected void AddError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }
    }
}
