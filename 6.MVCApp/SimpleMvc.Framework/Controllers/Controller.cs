using SimpleMvc.Framework.Contracts;
using SimpleMvc.Framework.Contracts.Generic;
using SimpleMvc.Framework.Helpers;
using SimpleMvc.Framework.ViewEngine;
using SimpleMvc.Framework.ViewEngine.Generic;
using System.Runtime.CompilerServices;

namespace SimpleMvc.Framework.Controllers
{
    public abstract class Controller
    {
        protected IActionResult View([CallerMemberName]string caller = "")
        {
            var controllerName = ControllerHelpers.GetControllerName(this);

            var viewFullQualifiedName = ControllerHelpers.GetFullQualifiedName(controllerName, caller);

            return new ActionResult(viewFullQualifiedName);
        }

        protected IActionResult View(string controller, string action)
        {
            var viewFullQualifiedName = ControllerHelpers.GetFullQualifiedName(controller, action);

            return new ActionResult(viewFullQualifiedName);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName]string caller = "")
        {
            var controllerName = ControllerHelpers.GetControllerName(this);

            var viewFullQualifiedName = ControllerHelpers.GetFullQualifiedName(controllerName, caller);

            return new ActionResult<TModel>(viewFullQualifiedName, model);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, string controller,string action)
        {
            var viewFullQualifiedName = ControllerHelpers.GetFullQualifiedName(controller, action);

            return new ActionResult<TModel>(viewFullQualifiedName, model);
        }
    }
}
