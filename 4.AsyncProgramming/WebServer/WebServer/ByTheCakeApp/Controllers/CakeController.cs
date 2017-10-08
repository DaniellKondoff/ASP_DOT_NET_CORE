using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.ByTheCakeApp.Models;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class CakeController : ControllerBase
    {
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
        {
            return this.FileViewResponse(@"cake\add", new Dictionary<string, string>
            {
                ["display"] = "none"
            });
        }

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);

            using (var streamWriter = new StreamWriter(@"ByTheCakeApp\Data\DataBase.csv",true))
            {
                streamWriter.WriteLine($"{name},{price}");
            }

            return this.FileViewResponse(@"Cake\add", new Dictionary<string, string>
            {
                ["name"] = name,
                ["price"] = price,
                ["display"] = "block"
            });
        }

        public IHttpResponse Search(IDictionary<string,string> urlParameters)
        {
            const string searchTermKey = "search";

            var results = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                var savedCakesDivs = File.ReadLines(@"ByTheCakeApp\Data\DataBase.csv")
                    .Where(l=>l.Contains(','))
                    .Select(l => l.Split(','))
                    .Select(l => new Cake
                    {
                        Name = l[0],
                        Price = decimal.Parse(l[1])
                    })
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $"<div>{c.Name} - ${c.Price}</div>");


                results = string.Join(Environment.NewLine, savedCakesDivs);
            }

            return this.FileViewResponse(@"Cake\search", new Dictionary<string, string>
            {
                ["results"] = results
            });
        }
    }
}
