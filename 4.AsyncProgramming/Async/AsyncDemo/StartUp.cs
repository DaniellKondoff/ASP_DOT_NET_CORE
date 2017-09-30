using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            //Task.Run(async () =>
            //{
            //    await DownloadFileAsync();
            //})
            //.GetAwaiter()
            //.GetResult();

            //Task.Run(async() =>
            //{
            //    var httpClient = new HttpClient();
            //    var result =  await httpClient.GetStringAsync("http://softuni.bg");
            //    Console.WriteLine(result);
            //})
            //.GetAwaiter()
            //.GetResult();

            //Task.Run(async() =>
            //{
            //   await GetHeaders("http://softuni.bg");
            //})
            //.GetAwaiter()
            //.GetResult();

            //var text = "Hello Worlds!";
            //var bytes = Encoding.UTF8.GetBytes(text);
            //var result = Encoding.UTF8.GetString(bytes);

            //Task.Run(async() =>
            //{
            //    using (var reader = new StreamReader("index.html"))
            //    {
            //        while (true)
            //        {
            //            var line = await reader.ReadLineAsync();

            //            if (line == null)
            //            {
            //                break;
            //            }

            //            Console.WriteLine(line);

            //        }
            //    }
            //})
            //.GetAwaiter()
            //.GetResult();

        }

        public static async Task DownloadFileAsync()
        {
            var webClient = new WebClient();

            Console.WriteLine("DownLoading....");

            await webClient.DownloadFileTaskAsync("https://blogs.msdn.microsoft.com/dotnet/2017/01/19/net-core-image-processing/", "index.html");

            Console.WriteLine("Finished!");
        }

        public static async Task GetHeaders(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                var headers = response.Headers;
                foreach (var header in headers)
                {
                    Console.WriteLine($"{header.Key}: {string.Join(", ",header.Value)}");
                }

                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(content);
            }
        }
    }
}
