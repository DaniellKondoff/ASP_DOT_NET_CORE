using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        HttpHeaderCollection Headers { get; }
        HttpStatusCode StatusCode { get; }
    }
}
