using ModPanel.App.Data.Models;
using ModPanel.App.Models.Users;
using ModPanel.App.Services;
using ModPanel.App.Services.Contracts;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Controllers
{
    public class UsersController : BaseController
    {
        private const string RegisterError = @"<p>Check Your form for errors.</p>
        <p> E-mails must have at least one '@' and one '.'</p>
        <p>Password must be at least 6 symbols and must contain at least 1 uppercase, 1 lowercase letter and 1 digit</p>
        <p>Confirm Password must match the provided password</p>";
        private const string EmailExistsError = "<p>E-mail is already taken</p>";
        private const string LoginError = "<p>Invalid Credentials</p>";
        private const string UserIsNotApprovedError = "<strong>You must wait for your registration to be approved!</strong>";

        private readonly IUserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }

        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword || !this.IsValidModel(model))
            {
                this.ShowError(RegisterError);
                return View();
            }

            var result = this.userService.Create(model.Email, model.Password, (PositionType)model.Position);

            if (result)
            {
                this.SignIn(model.Email);
                return this.RedirectToLogin();
            }
            else
            {
                this.ShowError(EmailExistsError);
                return View();
            }

        }

        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError(LoginError);
                return this.View();
            }
            if (!this.userService.UserIsApproved(model.Email))
            {
                this.ShowError(UserIsNotApprovedError);
                return this.View();
            }
            if (this.userService.UserExist(model.Email, model.Password))
            {
                this.SignIn(model.Email);
                return this.RedirectToHome();
            }      
            else
            {
                this.ShowError(LoginError);
                return this.View();
            }
        }

        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToHome();
        }
    }
}
