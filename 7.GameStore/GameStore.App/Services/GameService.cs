using GameStore.App.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using GameStore.App.Models.Games;
using GameStore.App.Data;
using System.Linq;
using GameStore.App.Data.Models;

namespace GameStore.App.Services
{
    public class GameService : IGameService
    {
        public IEnumerable<GameListingAdminModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Select(g => new GameListingAdminModel
                    {
                        Id = g.Id,
                        Name = g.Title,
                        Price = g.Price,
                        Size = g.Size
                    })
                    .ToList();
            }
        }

        public void Create(string title, string decription, string thumbnail, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Title = title,
                    Description = decription,
                    Thumbnail = thumbnail,
                    Price = price,
                    Size = size,
                    VideoId = videoId,
                    ReleaseDate = releaseDate
                };

                db.Games.Add(game);
                db.SaveChanges();
            }
        }

        public Game GetById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games.FirstOrDefault(g => g.Id == id);
            }
        }

        public void Update(int id, string title, string description, string thumbnail, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.Find(id);

                game.Title = title;
                game.Description = description;
                game.Thumbnail = thumbnail;
                game.Price = price;
                game.Size = size;
                game.VideoId = videoId;
                game.ReleaseDate = releaseDate;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.FirstOrDefault(g => g.Id == id);

                db.Games.Remove(game);
                db.SaveChanges();
            }
        }
    }
}
