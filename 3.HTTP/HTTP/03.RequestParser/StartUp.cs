using System;
using System.Collections.Generic;

namespace _03.RequestParser
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var validUrls = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var urlParts = line.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                var path = $"/{urlParts[0]}";
                var method = urlParts[1];

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }
                validUrls[path].Add(method);
            }

            var request = Console.ReadLine();
            var requestParts =request.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var requestMethod = requestParts[0];
            var requestUrl = requestParts[1];
            var requestProtocol = requestParts[2];

            var responsStatus = 404;
            var responsStatusText = "Not Found";

            if (validUrls.ContainsKey(requestUrl) && validUrls[requestUrl].Contains(requestMethod.ToLower()))
            {
                responsStatus = 200;
                responsStatusText = "OK";
            }

            Console.WriteLine($"{requestProtocol} {responsStatus} {responsStatusText}");
            Console.WriteLine($"Content-Length: {responsStatusText.Length}");
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine(responsStatusText);
        }
    }
}
