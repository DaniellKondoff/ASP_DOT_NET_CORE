using System;
using System.Collections.Generic;
using System.Text;
using WebServer.ByTheCakeApp.ViewModels.Account;

namespace WebServer.ByTheCakeApp.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string username, string password);

        bool Find(string username, string password);

        ProfileUserViewModel Profile(string username);

        int? GetUserId(string username);
    }
}
