using System;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class AccountController : ControllerBase
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["display"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Account\login");
        }

        public IHttpResponse Login(string username, string password)
        {
            this.ViewData["username"] = username;
            this.ViewData["password"] = password;
            this.ViewData["display"] = "block";

            return this.FileViewResponse(@"Account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "username";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey) || !req.FormData.ContainsKey(formPasswordKey))
            {
                return new BadRequestResponse();
            }

            string name = req.FormData[formNameKey];
            string password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "You have empty fields";
                return this.FileViewResponse(@"Account\login");
            }

            req.Session.Add(SessionStore.CurrentUserKey, name);
            req.Session.Add(ShoppingCard.SessionKey, new ShoppingCard());


            return new RedirectResponse("/");
        }

        internal IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();

            return new RedirectResponse(@"\login");
        }
    }
}
