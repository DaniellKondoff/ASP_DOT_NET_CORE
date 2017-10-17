using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.Infrastructure
{
    public abstract class Controller
    {
        public const string DefaultPath = @"{0}\Resource\{1}.html";
        public const string ContentPlaceholder = @"{{{content}}}";

        protected IDictionary<string,string> ViewData { get; private set; }

        protected Controller()
        {
            this.ViewData = new Dictionary<string, string>()
            {
                ["anonymousDisplay"]="none",
                ["authDisplay"] = "flex",
                ["showError"] = "none"
            };
        }

        protected abstract string ApplicationDirectory { get; }

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
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath,this.ApplicationDirectory, "layout"));
            var fileHtml = File.ReadAllText(string.Format(DefaultPath, this.ApplicationDirectory, fileName));

            var resultHtml = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return resultHtml;
        }

        protected IHttpResponse RedirectResponse (string route)
        {
            return new RedirectResponse(route);
        }

        protected void ShowError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        protected bool ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        this.ShowError(result.ErrorMessage);
                        return false;
                    }
                }
            }

            return true;
        }
    }
    
}
