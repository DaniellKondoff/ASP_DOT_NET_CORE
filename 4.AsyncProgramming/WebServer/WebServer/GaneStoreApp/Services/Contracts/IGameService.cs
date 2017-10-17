using System;
using System.Collections.Generic;
using System.Text;
using WebServer.GaneStoreApp.ViewModels.Admin;

namespace WebServer.GaneStoreApp.Services.Contracts
{
    public interface IGameService
    {
        void Create(string title, string description,string image,decimal price,double size, string videoId,DateTime releaseDate);

        IEnumerable<AdminListViewModel> All();

        AddGameViewModel Find(int id);

        void Edit(int id, string title, string description, string image, decimal price, double size, string videoId, DateTime releaseDate);

        bool Delete(int Id);
    }
}
