using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.GaneStoreApp.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string email, string name, string password);

        bool Find(string email, string password);

        bool IsAdmin(string email);
    }
}
