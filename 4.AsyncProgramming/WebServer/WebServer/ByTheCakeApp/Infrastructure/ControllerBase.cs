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


        public IHttpResponse FileViewResponse(string fileName)
        {
            var resultHtml = ProccessFileHtml(fileName);

            return new ViewResponse(HttpStatusCode.OK, new FileView(resultHtml));
        }

        public IHttpResponse FileViewResponse(string fileName,Dictionary<string,string> values)
        {
            var resultHtml = ProccessFileHtml(fileName);

            if (values != null && values.Any())
            {
                foreach (var value in values)
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
    }
}
