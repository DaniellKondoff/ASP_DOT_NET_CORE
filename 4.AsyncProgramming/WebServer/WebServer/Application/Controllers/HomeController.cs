using System;
using WebServer.Application.Views.Home;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;
using System.Net;
using WebServer.Server.HTTP;

namespace WebServer.Application.Controllers
{
    public class HomeController
    {
        //GET/
        public IHttpResponse Index()
        {
            var response =  new ViewResponse(HttpStatusCode.OK, new IndexView());

            response.Cookies.Add(new HttpCookie("lang", "en"));

            return response;
        }

        internal IHttpResponse About()
        {
            throw new NotImplementedException();
        }
    }
}
