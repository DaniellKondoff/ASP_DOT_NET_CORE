using WebServer.Server.Contracts;

namespace WebServer.ByTheCakeApp.Views.Home
{
    public class FileView : IView
    {
        private readonly string htmlFIle;

        public FileView(string htmlFIle)
        {
            this.htmlFIle = htmlFIle;
        }

        public string View()
        {
            return htmlFIle;
        }
    }
}
