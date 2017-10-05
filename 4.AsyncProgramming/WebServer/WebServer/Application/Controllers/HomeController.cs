using System;
using WebServer.Application.Views.Home;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;
using System.Net;

namespace WebServer.Application.Controllers
{
    public class HomeController
    {
        //GET/
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.OK, new IndexView());
        }

        internal IHttpResponse About()
        {
            throw new NotImplementedException();
        }
    }
}
