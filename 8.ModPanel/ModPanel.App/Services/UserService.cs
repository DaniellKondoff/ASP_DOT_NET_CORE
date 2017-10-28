using ModPanel.App.Data;
using ModPanel.App.Data.Models;
using ModPanel.App.Models.Admin;
using ModPanel.App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModPanel.App.Services
{
    public class UserService : IUserService
    {
        public bool Create(string email, string password, PositionType position)
        {
            using (var db = new ModePanelDbContext())
            {
                if (db.Users.Any(u => u.Email == email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any();

                var user = new User
                {
                    Email = email,
                    Password = password,
                    IsAdmin = isAdmin,
                    Position = position,
                    IsApproved = isAdmin
                };

                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool UserExist(string email, string password)
        {
            using (var db = new ModePanelDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Email == email && u.Password == password);
            }
        }

        public bool UserIsApproved(string email)
        {
            using (var db = new ModePanelDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Email == email && u.IsApproved);
            }
        }

        public IEnumerable<AdminUserModel> All()
        {
            using (var db = new ModePanelDbContext())
            {
                return db
                    .Users
                    .Select(u => new AdminUserModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Position = u.Position,
                        IsApproved = u.IsApproved,
                        Posts = u.Post.Count
                    })
                    .ToList();
            }
        }

        public string Approved(int id)
        {
            using (var db = new ModePanelDbContext())
            {
                var user = db.Users.Find(id);

                if (user != null)
                {
                    user.IsApproved = true;
                }

                db.SaveChanges();

                return user.Email;
            }
        }
    }
}
