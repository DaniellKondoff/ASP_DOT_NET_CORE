using GameStore.App.Data.Models;
using GameStore.App.Models.Games;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.App.Services.Contracts
{
    public interface IGameService
    {
        IEnumerable<GameListingAdminModel> All();

        void Create
            (string title, string decription, string thumbnail, decimal price, double size, string videoId, DateTime releaseDate);

        Game GetById(int id);

        void Update(int id, string title, string description, string thumbnail, decimal price, double size, string videoId, DateTime releaseDate);

        void Delete(int id);
             
    }
}
