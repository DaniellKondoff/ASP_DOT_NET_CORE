namespace WebServer.Application.Views.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Server.Contracts;

    public class IndexView : IView
    {
        public string View()
        {
            return "<h1>Welcome!</h1>";
        }
    }
}
