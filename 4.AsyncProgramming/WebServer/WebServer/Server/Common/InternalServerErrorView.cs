using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Contracts;

namespace WebServer.Server.Common
{
    public class InternalServerErrorView : IView
    {
        private readonly Exception ex;

        public InternalServerErrorView(Exception ex)
        {
            this.ex = ex;
        }

        public string View()
        {
            return $"<h1>{this.ex.Message}</h1><h2>{this.ex.StackTrace}</h2>";
        }
    }
}
