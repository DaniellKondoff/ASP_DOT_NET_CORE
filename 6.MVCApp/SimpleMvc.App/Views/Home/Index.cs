using SimpleMvc.Framework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.App.Views.Home
{
    public class Index : IRenderable
    {
        public string Render() => "<h1>Hello MVC!</h1>";
    }
}
