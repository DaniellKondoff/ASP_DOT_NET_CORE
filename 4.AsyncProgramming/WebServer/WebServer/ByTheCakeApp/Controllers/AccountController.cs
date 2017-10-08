using System;
using System.Collections.Generic;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class AccountController : ControllerBase
    {
        public IHttpResponse Login()
        {
            return this.FileViewResponse(@"Account\login", new Dictionary<string, string>
            {
                ["display"] = "none"
            });
        }

        public IHttpResponse Login(string username, string password)
        {
            return this.FileViewResponse(@"Account\login", new Dictionary<string, string>
            {
                ["username"] = username,
                ["password"] = password,
                ["display"] = "block"
            });
        }
    }
}
