using System;
using System.Net;

namespace _01.URLDecode
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var inputLine = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(inputLine);

            Console.WriteLine(decodedUrl);
        }
    }
}
