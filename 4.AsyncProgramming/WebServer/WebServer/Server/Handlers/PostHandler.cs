namespace WebServer.Server.Handlers
{
    using System;
    using WebServer.Server.HTTP.Contracts;

    public class PostHandler : RequestHandler
    {
        public PostHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) : base(handlingFunc)
        {
        }
    }
}
