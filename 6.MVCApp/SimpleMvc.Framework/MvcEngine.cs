using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SimpleMvc.Framework
{
    public static class MvcEngine
    {
        public static void Run(WebServer.WebServer server)
        {
            RegisterAssemblyName();
            try
            {
                server.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName = Assembly
                .GetEntryAssembly()
                .GetName()
                .Name;
        }
    }
}
