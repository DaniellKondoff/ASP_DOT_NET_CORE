using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Services.Contracts
{
    public interface IUserService
    {
        bool Create(string email, string password, string fullName);

        bool UserExist(string email, string password);
    }
}
