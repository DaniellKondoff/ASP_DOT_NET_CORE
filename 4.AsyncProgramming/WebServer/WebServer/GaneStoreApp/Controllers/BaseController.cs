using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GaneStoreApp.Common;
using WebServer.GaneStoreApp.Services;
using WebServer.GaneStoreApp.Services.Contracts;
using WebServer.Infrastructure;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.GaneStoreApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected const string HomePath = "/";

        private readonly IUserService userService;

        protected BaseController(IHttpRequest request)
        {
            this.Request = request;
            this.userService = new UserService();
            this.Authentication = new Authentication(false, false);
            this.ApplyAuthenticationViewData();
        }

        public IHttpRequest Request { get; private set; }

        protected override string ApplicationDirectory => "GaneStoreApp";

        protected Authentication Authentication { get; private set; }

        public void ApplyAuthenticationViewData()
        {
            var anonimousDisplay = "flex";
            var authDisplay = "none";
            var adminDisplay = "none";

            var isAuth = this.Request.Session.Contains(SessionStore.CurrentUserKey);

            if (isAuth)
            {
                anonimousDisplay = "none";
                authDisplay = "flex";

                var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);

                var isAdmin = this.userService.IsAdmin(email);
                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["anonymousDisplay"] = anonimousDisplay;
            this.ViewData["authDisplay"] = authDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
        }
    }
}
