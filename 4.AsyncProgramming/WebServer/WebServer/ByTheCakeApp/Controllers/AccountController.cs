using System;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.ByTheCakeApp.Services;
using WebServer.ByTheCakeApp.Services.Contracts;
using WebServer.ByTheCakeApp.ViewModels.Account;
using WebServer.Server.HTTP;
using WebServer.Server.HTTP.Contracts;
using WebServer.Server.HTTP.Response;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class AccountController : ControllerBase
    {
        private const string RegisterView = @"account\register";
        private const string LoginView = @"Account\login";

        private readonly IUserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }

        public IHttpResponse Login()
        {
            this.ViewData["display"] = "none";
            this.SetDefaultViewData();
            return this.FileViewResponse(@"Account\login");
        }

        //public IHttpResponse Login(string username, string password)
        //{
        //    this.ViewData["username"] = username;
        //    this.ViewData["password"] = password;
        //    this.ViewData["display"] = "block";

        //    return this.FileViewResponse(@"Account\login");
        //}

        public IHttpResponse Login(IHttpRequest req,LoginUserViewModel model)
        {
            string username = model.Username;
            string password = model.Password;

            var success = this.userService.Find(username, password);

            if (success)
            {
                this.LoginUser(req, username);

                return new RedirectResponse("/");
            }
            else
            {
                this.AddError("Invalid user details");

                return this.FileViewResponse(LoginView);
            }

        }

        private void LoginUser(IHttpRequest req, string name)
        {
            req.Session.Add(SessionStore.CurrentUserKey, name);
            req.Session.Add(ShoppingCard.SessionKey, new ShoppingCard());
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            if (!req.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("There is no logged in user.");
            }

            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = this.userService.Profile(username);

            if (profile == null)
            {
                throw new InvalidOperationException($"The user {username} could not be found in the Database");
            }

            this.ViewData["username"] = profile.Username;
            this.ViewData["registrationDate"] = profile.RegistrationDate.ToShortDateString();
            this.ViewData["totalOrders"] = profile.TotalOrders.ToString();

            return this.FileViewResponse(@"account\profile");
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            request.Session.Clear();

            return new RedirectResponse(@"\login");
        }

        public IHttpResponse Register()
        {
            this.SetDefaultViewData();
            return this.FileViewResponse(RegisterView);
        }

        public IHttpResponse Register(IHttpRequest req,RegisterUserViewModel model)
        {
            this.SetDefaultViewData();

            //Validate The model
            if (model.Username.Length <3 || model.Password.Length <3 || model.ConfirmPassword != model.Password )
            {
                this.AddError("Invalid user details !");

                return this.FileViewResponse(RegisterView);
            }

            var success = this.userService.Create(model.Username, model.Password);

            if (success)
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }
            else
            {
                this.AddError("This user name is taken");

                return this.FileViewResponse(RegisterView);
            }
        }

        private void SetDefaultViewData()
        {
            this.ViewData["authDisplay"] = "none";
        }
    }
}
