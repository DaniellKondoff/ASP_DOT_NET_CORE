using ModPanel.App.Data;
using ModPanel.App.Data.Models;
using ModPanel.App.Services;
using ModPanel.App.Services.Contracts;
using SimpleMvc.Framework.Contracts;
using SimpleMvc.Framework.Controllers;
using System.Linq;

namespace ModPanel.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ILogService logs;

        protected BaseController()
        {
            this.logs = new LogService();

            this.ViewModel["anonymousDisplay"] = "flex";
            this.ViewModel["userDisplay"] = "none";
            this.ViewModel["adminDisplay"] = "none";
            this.ViewModel["show-error"] = "none";
        }

        protected User Profile { get; private set; }

        protected bool IsAdmin => this.User.IsAuthenticated && this.Profile.IsAdmin;


        protected void ShowError(string error)
        {

            this.ViewModel["show-error"] = "block";
            this.ViewModel["error"] = error;
        }

        protected override void InitializeController()
        {
            base.InitializeController();

            if (this.User.IsAuthenticated)
            {
                this.ViewModel["anonymousDisplay"] = "none";
                this.ViewModel["userDisplay"] = "flex";

                using (var db = new ModePanelDbContext())
                {
                    this.Profile = db.Users
                        .First(u => u.Email == this.User.Name);

                    if (this.Profile.IsAdmin)
                    {
                        this.ViewModel["adminDisplay"] = "flex";
                    }
                }
            }
        }

        protected IActionResult RedirectToLogin()
        {
            return this.Redirect("/users/login");
        }

        protected IActionResult RedirectToHome()
        {
            return this.Redirect("/");
        }

        protected void Log(LogType type, string additionalInfo)
        {
            this.logs.Create(this.Profile.Email, type, additionalInfo);
        }

    }
}
