using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GaneStoreApp.Data;
using WebServer.GaneStoreApp.Data.Models;
using WebServer.GaneStoreApp.Services.Contracts;

namespace WebServer.GaneStoreApp.Services
{
    public class UserService : IUserService
    {
        public bool Create(string email, string name, string password)
        {
            using (var db = new GameStoreDbContext())
            {
                
                if (db.Users.Any( u => u.Email == email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any();

                var user = new User
                {
                    Email = email,
                    Name = name,
                    Password = password,
                    IsAdmin = isAdmin
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            return true;
        }

        public bool Find(string email, string password)
        {
            using (var db  = new GameStoreDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.Password == password);
            }
        }

        public bool IsAdmin(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Users.Any(u => u.Email == email && u.IsAdmin);
            }
        }
    }
}
