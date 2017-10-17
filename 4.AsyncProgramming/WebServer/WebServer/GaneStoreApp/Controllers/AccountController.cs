using System;
using System.Collections.Generic;
using WebServer.GaneStoreApp.Services;
using WebServer.GaneStoreApp.Services.Contracts;
using WebServer.GaneStoreApp.ViewModels.Account;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.GaneStoreApp.Controllers
{
    public class AccountController : BaseController
    {
        private const string RegisterViewPath = @"account\register";
        private const string LoginViewPath = @"account\login";
        private readonly IUserService userService;

        public AccountController(IHttpRequest request) 
            : base(request)
        {
            this.userService = new UserService();
        }

        // GET /account/register
        public IHttpResponse Register()
        {
            return this.FileViewResponse(RegisterViewPath);
        }

        // POST /account/register
        public IHttpResponse Register(RegisterViewModel model)
        {

            if (!this.ValidateModel(model))
            {
                return this.Register();
            }

            var success = this.userService.Create(model.Email, model.FullName, model.Password);

            if (!success)
            {
                this.ShowError("Email is taken.");

                return this.Register();
            }
            else
            {
                this.LoginUser(model.Email);
                return this.RedirectResponse(HomePath);
            }
        }

        public IHttpResponse Login()
        {

            return this.FileViewResponse(LoginViewPath);
        }

        public IHttpResponse Login(LoginViewModel model)
        {
            if (!this.ValidateModel(model))
            {
                return this.Login();
            }

            var success = this.userService.Find(model.Email, model.Password);

            if (!success)
            {
                this.ShowError("Invalid User Details");

                return this.Login();
            }
            else
            {
                this.LoginUser(model.Email);
                return this.RedirectResponse(HomePath);
            }
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return this.RedirectResponse(HomePath);
        }

        private void LoginUser(string email)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, email);
        }

    }
}
