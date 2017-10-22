using Microsoft.AspNetCore.Mvc;
using MVCDemo.Data;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookData books;

        public BooksController()
        {
            this.books = new BookData();
        }

        //GET /Books/All
        //public object All()
        //{
        //    return new List<BookListingModel>
        //    {
        //        new BookListingModel{Id=1 , Name="Name 1", Author="Author 1"},
        //        new BookListingModel{Id=2 , Name="Name 2", Author="Author 2"},
        //        new BookListingModel{Id=3 , Name="Name 3", Author="Author 3"}
        //    };
        //}

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateBookModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            this.books.Create(model.Title, model.Year, model.Author);

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            return null;
        }
    }
}
