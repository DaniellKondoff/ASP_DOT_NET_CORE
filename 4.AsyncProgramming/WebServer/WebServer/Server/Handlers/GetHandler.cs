namespace WebServer.Server.Handlers
{
    using System;
    using WebServer.Server.HTTP.Contracts;

    public class GetHandler : RequestHandler
    {
        public GetHandler(Func<IHttpRequest, IHttpResponse> handlingFunc) : base(handlingFunc)
        {
        }
    }
}
