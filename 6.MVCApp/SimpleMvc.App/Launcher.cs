using Microsoft.EntityFrameworkCore;
using SimpleMvc.Framework;
using SimpleMvc.Framework.Routers;
using SImpleMvc.Data;
using System;

namespace SimpleMvc.App
{
    class Launcher
    {
        static void Main(string[] args)
        {
            using (var db = new NoteDbContext())
            {
                db.Database.Migrate();
            }
            MvcEngine.Run(new WebServer.WebServer(1337,new ControllerRouter()));
        }
    }
}
