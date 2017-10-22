using SimpleMvc.Framework.Contracts.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Framework.ViewEngine.Generic
{
    public class ActionResult<TModel> : IActionResult<TModel>
    {
        public ActionResult(string viewFullQualifiedName, TModel model)
        {
            this.Action = Activator.
                CreateInstance(Type.GetType(viewFullQualifiedName))
                as IRenderable<TModel>;

            if (this.Action == null)
            {
                throw new InvalidOperationException("The given view does not implement IRenderable<TMode>");
            }
            this.Action.Model = model;
        }

        public IRenderable<TModel> Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}
