using ModPanel.App.Models.Admin;
using ModPanel.App.Models.Home;
using ModPanel.App.Models.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModPanel.App.Services.Contracts
{
    public interface IPostService
    {
        void Create(string title, string content,int userId);

        IEnumerable<AdminPostModel> All();

        IEnumerable<HomeListingModel> AllWithDetails(string search = null);

        PostModel GetById(int id);
        void Update(int id, string title, string content);
        string Delete(int id);
    }
}
