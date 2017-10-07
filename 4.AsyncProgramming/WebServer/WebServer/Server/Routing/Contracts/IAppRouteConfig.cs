namespace WebServer.Server.Routing.Contracts
{
    using System.Collections.Generic;
    using Enums;
    using Handlers;
    using WebServer.Server.HTTP.Contracts;
    using System;

    // GET /home - GetHandler
    // GET /about - GetHandler
    // POST /home - PostHandler
    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod,IDictionary<string,RequestHandler>> Routes { get; }
        void Get(string route, Func<IHttpRequest, IHttpResponse> handler);
        void Post(string route, Func<IHttpRequest, IHttpResponse> handler);
        void AddRoute(string route, HttpRequestMethod method, RequestHandler httpHandler);
    }
}
