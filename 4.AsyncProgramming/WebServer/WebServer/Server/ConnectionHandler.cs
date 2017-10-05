namespace WebServer.Server
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using Common;
    using Routing.Contracts;
    using System.Threading.Tasks;
    using WebServer.Server.HTTP;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.Handlers;
    using System.Text;

    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client,IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProccesRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var httpContext = new HttpContext(httpRequest);

                var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

                var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

                var byteSegment = new ArraySegment<byte>(responseBytes);

                await this.client.SendAsync(byteSegment, SocketFlags.None);

                Console.WriteLine("------Request-------");
                Console.WriteLine(httpRequest);
                Console.WriteLine("------RESPONSE------");
                Console.WriteLine(httpResponse);
                Console.WriteLine();
            }
            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();
            var dataa = new ArraySegment<byte>(new byte[1024]);


            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(dataa, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(dataa.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1024)
                {
                    break;
                }
            }

            if (result.Length ==0)
            {
                return null;
            }
            return new HttpRequest(result.ToString());
        }
    }
}
