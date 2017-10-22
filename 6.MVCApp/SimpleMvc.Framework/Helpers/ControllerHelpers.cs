using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Framework.Helpers
{
    public static class ControllerHelpers
    {
        public static string GetFullQualifiedName(
            string controllerName, string action)
        {
            return string.Format(
                "{0}.{1}.{2}.{3}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ViewsFolder,
                controllerName,
                action);
        }

        public static string GetControllerName(object controller)
        {
            return controller.GetType()
                .Name.Replace(MvcContext.Get.ControllersSuffix, string.Empty);
        }
    }
}
