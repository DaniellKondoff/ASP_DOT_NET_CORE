using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.GaneStoreApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpRequest request) 
            : base(request)
        {

        }

        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");        
        }
    }
}
