namespace WebServer.Server.Handlers
{
    using System;
    using Contracts;
    using HTTP.Contracts;
    using HTTP;
    using Common;

    public  class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> handlingFunc;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunc)
        {
            CoreValidator.ThrowIfNull(handlingFunc, nameof(handlingFunc));
            this.handlingFunc = handlingFunc;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);

            if (!httpResponse.Headers.ContainsKey(HttpHeader.ContentType))
            {
                httpResponse.Headers.Add(HttpHeader.ContentType, "text/plain");
            }

            foreach (var cookie in httpResponse.Cookies)
            {
                httpResponse.Headers.Add(HttpHeader.SetCookie, cookie.ToString());
            }

            return httpResponse;
        }
    }
}
