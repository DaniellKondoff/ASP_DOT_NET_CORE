namespace WebServer.Server.Handlers
{
    using System;
    using Contracts;
    using HTTP.Contracts;
    using HTTP;
    using Common;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);

            httpResponse.Headers.Add(new HttpHeader("Content-Type", "text/plain"));

            return httpResponse;
        }
    }
}
