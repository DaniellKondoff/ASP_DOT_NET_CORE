using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Data
{
    public class BookData
    {
        private static List<Book> allBooks = new List<Book>();

        public void Create(string title, int year,string author)
        {
            var id = allBooks.Count + 1;

            allBooks.Add(new Book
            {
                Title = title,
                Year = year,
                Author = author
            });
        }
    }
}
