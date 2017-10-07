namespace WebServer
{
    using System;
    using WebServer.Application;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing;
    using WebServerr.Server;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        public static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new MainApplication();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.Configure(appRouteConfig);

            this.webServer = new WebServer(1337, appRouteConfig);

            this.webServer.Run();
        }
    }
}
