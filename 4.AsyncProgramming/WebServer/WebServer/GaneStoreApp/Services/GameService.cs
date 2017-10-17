using System;
using System.Collections.Generic;
using System.Linq;
using WebServer.GaneStoreApp.Data;
using WebServer.GaneStoreApp.Data.Models;
using WebServer.GaneStoreApp.Services.Contracts;
using WebServer.GaneStoreApp.ViewModels.Admin;

namespace WebServer.GaneStoreApp.Services
{
    public class GameService : IGameService
    {

        public void Create(string title, string description, string image, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = new Game
                {
                    Title = title,
                    Description = description,
                    Image = image,
                    Price = price,
                    Size = size,
                    VideoId = videoId,
                    ReleaseDate = releaseDate
                };

                db.Add(game);
                db.SaveChanges();
            }
        }

        public IEnumerable<AdminListViewModel> All()
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Select(g => new AdminListViewModel
                    {
                        Id = g.Id,
                        Name = g.Title,
                        Price = g.Price,
                        Size = g.Size
                    })
                    .ToList();
            }
        }

        public AddGameViewModel Find(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games
                    .Where(g => g.Id == id)
                    .Select(g => new AddGameViewModel
                    {
                        Title = g.Title,
                        Description = g.Description,
                        Image = g.Image,
                        Price = g.Price,
                        Size = g.Size,
                        VideoId = g.VideoId,
                        ReleaseDate = g.ReleaseDate
                    })
                    .FirstOrDefault();
            }
        }

        public void Edit(int id, string title, string description, string image, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var currentGame = db.Games.FirstOrDefault(g => g.Id == id);

                currentGame.Title = title;
                currentGame.Description = description;
                currentGame.Image = image;
                currentGame.Price = price;
                currentGame.Size = size;
                currentGame.VideoId = videoId;
                currentGame.ReleaseDate = releaseDate;

                db.SaveChanges();
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new GameStoreDbContext())
            {
                if (db.Games.Any(g=>g.Id == Id))
                {
                    var game = db.Games.FirstOrDefault(g => g.Id == Id);
                    db.Games.Remove(game);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}
