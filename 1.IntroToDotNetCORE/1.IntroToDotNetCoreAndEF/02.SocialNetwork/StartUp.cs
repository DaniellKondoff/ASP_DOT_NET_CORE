using _02.SocialNetwork.Data;
using System;
using System.Linq;

namespace _02.SocialNetwork
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new SocialNetworkDB())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //Task 2
                //PrintAllUsersWithTheirFriends(db);
                //PrintAllActiveUsersWithMoreThan5(db);

                //Task 3
                PrintAllAlbums(db);
                db.SaveChanges();
            }
        }

        private static void PrintAllAlbums(SocialNetworkDB db)
        {
            var albums = db.Albums
                 .Select(a => new
                 {
                     Title = a.Name,
                     OwnerName = a.User.Username,
                     PicturesCount = a.Pictures.Count
                 })
                 .OrderByDescending(a => a.PicturesCount)
                 .ThenBy(a => a.OwnerName)
                 .ToList();
        }

        private static void PrintAllActiveUsersWithMoreThan5(SocialNetworkDB db)
        {
            var users = db.Users
                 .Where(u => u.IsDeleted)
                 .Where(u => u.Friends.Count > 5)
                 .OrderBy(u => u.RegisteredOn)
                 .ThenByDescending(u => u.Friends.Count)
                 .Select(u => new
                 {
                     u.Username,
                     FriendCount = u.Friends.Count,
                     Period = (DateTime.Now - u.RegisteredOn).Days
                 })
                 .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} {user.FriendCount} {user.Period}");
            }
        }

        private static void PrintAllUsersWithTheirFriends(SocialNetworkDB db)
        {
            var users = db.Users
                .Select(u => new
                {
                    u.Username,
                    FriendsCount = u.Friends.Count,
                    Status = u.IsDeleted
                })
                .OrderByDescending(u => u.FriendsCount)
                .ThenBy(u => u.Username)
                .ToList();

            foreach (var user in users)
            {
                string status = user.Status == false ? "Active" : "Inactive";
                Console.WriteLine($"{user.Username} {user.FriendsCount} {status}");
            }
        }
    }
}
