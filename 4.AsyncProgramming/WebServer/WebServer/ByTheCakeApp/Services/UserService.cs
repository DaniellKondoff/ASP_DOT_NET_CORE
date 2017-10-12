using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.ByTheCakeApp.Data;
using WebServer.ByTheCakeApp.Data.Models;
using WebServer.ByTheCakeApp.Services.Contracts;
using WebServer.ByTheCakeApp.ViewModels.Account;

namespace WebServer.ByTheCakeApp.Services
{
    public class UserService : IUserService
    {
        public bool Create(string username, string password)
        {
            using(var db = new CakeDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Find(string username, string password)
        {
            using(var db = new CakeDbContext())
            {
                return db.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public int? GetUserId(string username)
        {
            using (var db = new CakeDbContext())
            {
                var id = db.Users
                    .Where(u => u.Username == username)
                    .Select(u => u.Id)
                    .FirstOrDefault();

                return id != 0 ? (int?)id : null;
            }
        }

        public ProfileUserViewModel Profile(string username)
        {
            using (var db = new CakeDbContext())
            {
                return db
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => new ProfileUserViewModel
                    {
                        Username = u.Username,
                        RegistrationDate = u.RegistrationDate,
                        TotalOrders = u.Orders.Count
                    })
                    .FirstOrDefault();
            }
        }
    }
}
