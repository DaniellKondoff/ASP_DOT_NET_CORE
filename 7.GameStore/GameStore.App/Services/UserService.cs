using GameStore.App.Data;
using GameStore.App.Data.Models;
using GameStore.App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.App.Services
{
    public class UserService : IUserService
    {
        public bool Create(string email, string password, string fullName)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Users.Any(u=>u.Email==email))
                {
                    return false;
                }

                var isAdmin = !db.Users.Any();

                var user = new User
                {
                    Name = fullName,
                    Email = email,
                    Password = password,
                    IsAdmin = isAdmin
                };

                db.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool UserExist(string email,string password)
        {
            using (var db = new GameStoreDbContext())
            {
                return db
                    .Users
                    .Any(u => u.Email == email && u.Password == password);
            }
        }
    }
}
