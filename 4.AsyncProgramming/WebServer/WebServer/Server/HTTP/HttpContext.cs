using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Common;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.Server.HTTP
{
    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(IHttpRequest request)
        {
            CoreValidator.ThrowIfNull(request, nameof(request));

            this.request = request;
        }
        public IHttpRequest Request => this.request;
    }
}
