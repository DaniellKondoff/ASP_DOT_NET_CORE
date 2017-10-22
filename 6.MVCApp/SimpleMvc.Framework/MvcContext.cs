using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Framework
{
    public class MvcContext
    {
        private static MvcContext instance;

        private MvcContext()
        {

        }

        public static MvcContext Get => instance == null ? (instance = new MvcContext()) : instance;

        public string AssemblyName { get; set; }

        public string ControllersFolders { get; set; } = "Controllers";

        public string ControllersSuffix { get; set; } = "Controller";

        public string ViewsFolder { get; set; } = "Views";

        public string ModelsFolder { get; set; } = "Models";

    }
}
