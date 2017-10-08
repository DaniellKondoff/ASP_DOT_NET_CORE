using WebServer.Server.HTTP.Contracts;
using WebServer.ByTheCakeApp.Infrastructure;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class HomeController : ControllerBase
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
