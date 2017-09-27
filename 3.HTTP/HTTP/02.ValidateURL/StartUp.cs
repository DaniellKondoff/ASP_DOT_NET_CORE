using System;
using System.Net;

namespace _02.ValidateURL
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var inputLine = Console.ReadLine();

            var url = WebUtility.UrlDecode(inputLine);
            var uri = new Uri(url);

            if (string.IsNullOrEmpty(uri.Scheme) || string.IsNullOrEmpty(uri.Host) || uri.Port == 0 || string.IsNullOrEmpty(uri.LocalPath))
            {
                Console.WriteLine("Invalid");
            }
            else
            {
                Console.WriteLine(uri.Scheme);
                Console.WriteLine(uri.Host);
                Console.WriteLine(uri.Port);
                Console.WriteLine(uri.LocalPath);
                Console.WriteLine(uri.Query);
                Console.WriteLine(uri.Fragment);
            }
        }
    }
}
