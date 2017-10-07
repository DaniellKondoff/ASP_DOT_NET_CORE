using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebServer.Server.Common;

namespace WebServer.Server.HTTP.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl)
        {
            CoreValidator.ThrowIfNull(redirectUrl, nameof(redirectUrl));

            this.StatusCode = HttpStatusCode.Found;
            this.Headers.Add(HttpHeader.Location, redirectUrl);
        }
    }
}
