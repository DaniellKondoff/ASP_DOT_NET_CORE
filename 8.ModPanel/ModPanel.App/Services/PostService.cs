using ModPanel.App.Data;
using ModPanel.App.Data.Models;
using ModPanel.App.Services.Contracts;
using System.Collections.Generic;
using ModPanel.App.Models.Admin;
using System.Linq;
using ModPanel.App.Models.Posts;
using ModPanel.App.Models.Home;

namespace ModPanel.App.Services
{
    public class PostService : IPostService
    {
        public IEnumerable<AdminPostModel> All()
        {
            using (var db = new ModePanelDbContext())
            {
                return db.Posts
                    .Select(p => new AdminPostModel
                    {
                        Id = p.Id,
                        Title = p.Title
                    })
                    .ToList();
            }
        }

        public IEnumerable<HomeListingModel> AllWithDetails(string search = null)
        {
            using (var db = new ModePanelDbContext())
            {
                var query = db.Posts.AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(p => p.Title.ToLower().Contains(search.ToLower()));
                }

                return query
                    .OrderByDescending(p=>p.Id)
                    .Select(p => new HomeListingModel
                    {
                        Title = p.Title,
                        Content = p.Content,
                        CreatedBy = p.User.Email
                    })
                    .ToList();
            }
        }

        public void Create(string title, string content,int userId)
        {
            using (var db = new ModePanelDbContext())
            {
                var post = new Post
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }

        public string Delete(int id)
        {
            using (var db = new ModePanelDbContext())
            {
                var post = db.Posts.Find(id);

                if (post == null)
                {
                    return null;
                }

                db.Posts.Remove(post);
                db.SaveChanges();

                return post.Title;
            }
        }

        public PostModel GetById(int id)
        {
            using (var db = new ModePanelDbContext())
            {
                return db.Posts
                    .Where(p => p.Id == id)
                    .Select(p => new PostModel
                    {
                        Title = p.Title,
                        Content = p.Content
                    })
                    .FirstOrDefault();
            }
        }

        public void Update(int id, string title, string content)
        {
            using (var db = new ModePanelDbContext())
            {
                var post = db.Posts.Find(id);

                if (post== null)
                {
                    return;
                }

                post.Title = title;
                post.Content = content;

                db.SaveChanges();
            }
        }
    }
}
