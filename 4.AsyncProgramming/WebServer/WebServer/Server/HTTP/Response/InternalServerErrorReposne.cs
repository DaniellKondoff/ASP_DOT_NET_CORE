using System;
using System.Net;
using WebServer.Server.Common;

namespace WebServer.Server.HTTP.Response
{
    public class InternalServerErrorReposne : ViewResponse
    {
        public InternalServerErrorReposne(Exception ex)
            :base(HttpStatusCode.InternalServerError, new InternalServerErrorView(ex))
        {
        }
    }
}
