namespace WebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebServer.Server.Enums;
    using WebServer.Server.Handlers;
    using WebServer.Server.HTTP.Contracts;
    using WebServer.Server.Routing.Contracts;

    public class AppRouteConfig : IAppRouteConfig
    {

        private readonly Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> routes;

        public AppRouteConfig()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, RequestHandler>>();

            foreach (HttpRequestMethod requestMethod in Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>())
            {
                this.routes.Add(requestMethod, new Dictionary<string, RequestHandler>());
            }
        }

        public IReadOnlyDictionary<HttpRequestMethod, Dictionary<string, RequestHandler>> Routes => this.routes;

        public void Get(string route, Func<IHttpRequest,IHttpResponse> handler)
        {
            this.AddRoute(route, HttpRequestMethod.GET, new GetHandler(handler));
        }

        public void Post(string route, Func<IHttpRequest, IHttpResponse> handler)
        {
            this.AddRoute(route, HttpRequestMethod.POST, new PostHandler(handler));
        }

        public void AddRoute(string route, HttpRequestMethod method, RequestHandler httpHandler)
        {
            this.routes[method].Add(route, httpHandler);
        }
    }
}
