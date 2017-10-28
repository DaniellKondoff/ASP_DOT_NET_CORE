using ModPanel.App.Data.Models;
using ModPanel.App.Infrastructure;
using ModPanel.App.Models.Posts;
using ModPanel.App.Services;
using ModPanel.App.Services.Contracts;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModPanel.App.Controllers
{
    public class AdminController : BaseController
    {
        private const string EditeError = "<p>Invalid Post</p>";

        private readonly IUserService userService;
        private readonly IPostService postsService;
        private readonly ILogService logService;

        public AdminController()
        {
            this.userService = new UserService();
            this.postsService = new PostService();
            this.logService = new LogService();
        }
        public IActionResult Users()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var users = userService.All()
                .Select(u => $@"
                        <tr>
                        <td>{u.Id}</td>
                        <td>{u.Email}</td> 
                        <td>{u.Position.ToFriendlyName()}</td>
                        <td>{u.Posts}</td>
                        <td>{(u.IsApproved ? string.Empty : 
                        $@"<a class=""btn btn-primary"" href=""/admin/approve?id={u.Id}"">Approve</a>")}
                        </td>
                        </tr>");

            var resultHtml = string.Join(Environment.NewLine, users);

            ViewModel["users"] = resultHtml;

            this.Log(LogType.OpenMenu, nameof(Users));
            return this.View();
        }
        

        public IActionResult Approve(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var userEmail = this.userService.Approved(id);

            this.Log(LogType.UserApproval, userEmail);


            return this.Redirect("/admin/users");
        }

        public IActionResult Posts()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var posts = this.postsService
                .All()
                .Select(p => $@"<tr><td>{p.Id}</td><td>{p.Title}</td><td><a class=""btn btn-sm btn-warning"" href=""/admin/edit?id={p.Id}"">Edit</a> <a class=""btn btn-sm btn-danger"" href=""/admin/delete?id={p.Id}"">Delete</a></td></tr>");

            var postToHtml = string.Join(Environment.NewLine, posts);

            this.ViewModel["posts"] = postToHtml;
            this.Log(LogType.OpenMenu, nameof(Posts));


            return this.View();
        }

        public IActionResult Edit(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var post = this.postsService.GetById(id);
            if (post == null)
            {
                return this.NotFound();
            }

            this.ViewModel["title"] = post.Title;
            this.ViewModel["content"] = post.Content;

            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(int id,PostModel model)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }
            if (!this.IsValidModel(model) || !char.IsUpper(model.Title[0]))
            {
                this.ShowError(EditeError);
                return this.View();
            }

            this.postsService.Update(id, model.Title, model.Content);


            this.Log(LogType.EditPost, model.Title);
            
            return this.Redirect("/admin/posts");
        }

        public IActionResult Delete(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var post = this.postsService.GetById(id);
            if (post == null)
            {
                return this.NotFound();
            }

            this.ViewModel["id"] = id.ToString();
            this.ViewModel["title"] = post.Title;
            this.ViewModel["content"] = post.Content;
           
            return this.View();
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

           var postTitle =  this.postsService.Delete(id);

            this.Log(LogType.DeletePost, postTitle);


            return this.Redirect("/admin/posts");
        }

        public IActionResult Log()
        {
            this.Log(LogType.OpenMenu, nameof(Log));

            var logs = this.logService
                .All()
                .Select(l => $@"<div class=""card border-{l.Type.ToViewClassName()} mb - 1"">
                                    <div class=""card-body"">
                                        <p class=""card-text"">{l.ToString()}</p>
                                    </div>
                            </div>");

            this.ViewModel["logs"] = string.Join(string.Empty, logs);

            return this.View();
        }
    }
}
