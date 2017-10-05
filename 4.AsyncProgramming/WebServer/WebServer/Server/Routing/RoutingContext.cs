namespace WebServer.Server.Routing
{
    using System.Collections.Generic;
    using WebServer.Server.Common;
    using WebServer.Server.Handlers;
    using WebServer.Server.Routing.Contracts;

    public class RoutingContext : IRoutingContext
    {
        public RoutingContext(RequestHandler requestHandler, IEnumerable<string> parameters)
        {
            CoreValidator.ThrowIfNull(requestHandler, nameof(requestHandler));
            CoreValidator.ThrowIfNull(parameters, nameof(parameters));

            this.Parameters = parameters;
            this.RequestHandler = requestHandler;
        }
        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler RequestHandler { get; private set; }
    }
}
