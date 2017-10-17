using WebServer.Server.HTTP.Contracts;
using WebServer.Infrastructure;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home/index");
        }

        public IHttpResponse About()
        {
            return this.FileViewResponse(@"home/about");
        }
    }
}
