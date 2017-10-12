namespace WebServer.Server.Handlers
{
    using System;
    using System.Text.RegularExpressions;
    using Common;
    using Handlers.Contracts;
    using HTTP;
    using HTTP.Contracts;
    using HTTP.Response;
    using Routing.Contracts;
    using System.Linq;

    public class HttpHandler : IRequestHandler
    {
        private readonly IServerRouteConfig serverRouteConfig;

        public HttpHandler(IServerRouteConfig routeConfig)
        {
            CoreValidator.ThrowIfNull(routeConfig, nameof(routeConfig));

            this.serverRouteConfig = routeConfig;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            try
            {
                var anonymousPaths = new[] { "/login", "/register" };

                //CHeck if user is authenticated
                if  (!anonymousPaths.Contains(httpContext.Request.Path) && !httpContext.Request.Session.Contains(SessionStore.CurrentUserKey))
                {
                    return new RedirectResponse(anonymousPaths.First());
                }

                var requestMethod = httpContext.Request.Method;
                var requestPath = httpContext.Request.Path;
                var registeredRoutes = this.serverRouteConfig.Routes[requestMethod];

                foreach (var registeredRoute in registeredRoutes)
                {
                    // ^/users/(?<name>[a-b]+)$
                    var routePattern = registeredRoute.Key;
                    var routingContext = registeredRoute.Value;

                    var routeRegex = new Regex(routePattern);
                    var match = routeRegex.Match(requestPath);

                    if (!match.Success)
                    {
                        continue;
                    }

                    var parameters = routingContext.Parameters;

                    foreach (var parameter in parameters)
                    {
                        var parameterValue = match.Groups[parameter].Value;

                        httpContext.Request.AddUrlParameter(parameter, parameterValue);
                    }

                    return routingContext.Handler.Handle(httpContext);
                }
            }
            catch(Exception e)
            {

                return new InternalServerErrorReposne(e);
            }

            return new NotFoundResponse();
        }
    }
}
