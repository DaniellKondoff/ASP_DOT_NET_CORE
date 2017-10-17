using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Infrastructure;

namespace WebServer.ByTheCakeApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override string ApplicationDirectory => "ByTheCakeApp";
    }
}
