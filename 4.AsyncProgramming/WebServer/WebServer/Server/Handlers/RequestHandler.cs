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
            string sessionIdToSend = null;

            if (!httpContext.Request.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var sessionId = Guid.NewGuid().ToString();

                httpContext.Request.Session = SessionStore.Get(sessionId);

                sessionIdToSend = sessionId;
            }

            IHttpResponse httpResponse = this.handlingFunc(httpContext.Request);

            if (sessionIdToSend != null)
            {
                httpResponse.Headers.Add(HttpHeader.SetCookie,
                    $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
            }

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
