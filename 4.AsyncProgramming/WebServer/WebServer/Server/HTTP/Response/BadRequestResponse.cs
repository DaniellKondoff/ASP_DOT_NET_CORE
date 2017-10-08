using System.Net;

namespace WebServer.Server.HTTP.Response
{
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
        {
            this.StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
