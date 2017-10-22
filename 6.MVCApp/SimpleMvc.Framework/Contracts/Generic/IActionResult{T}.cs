using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Framework.Contracts.Generic
{
    public interface IActionResult<TModel> : IInvocable
    {
        IRenderable<TModel> Action { get; set; }
    }
}
