using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebServer.ByTheCakeApp.Models;

namespace WebServer.ByTheCakeApp.Data
{
    public class CakesData
    {
        private const string DataBaseFilePath = @"ByTheCakeApp\Data\DataBase.csv";

        public IEnumerable<Cake> All()
        {
          return File
                .ReadLines(@"ByTheCakeApp\Data\DataBase.csv")
                .Where(l => l.Contains(','))
                .Select(l => l.Split(','))
                .Select(l => new Cake
                {
                    Id = int.Parse(l[0]),
                    Name = l[1],
                    Price = decimal.Parse(l[2])
                });
        }

        public void Add(string name, string price)
        {
            var streamReader = new StreamReader(DataBaseFilePath);
            var id = streamReader.ReadToEnd().Split(Environment.NewLine).Length;
            streamReader.Dispose();

            using (var streamWriter = new StreamWriter(DataBaseFilePath, true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }
        }

        public Cake Find(int id)
        {
            return this.All().FirstOrDefault(c => c.Id == id);
        }

    }
}
