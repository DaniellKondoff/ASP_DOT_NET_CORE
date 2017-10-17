using System.Collections.Generic;
using System.Linq;
using WebServer.Infrastructure;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class CalculatorController : BaseController
    {
        public IHttpResponse Calculate()
        {
            this.ViewData["display"] = "none";
            return this.FileViewResponse(@"calculate\calc");

        }

        public IHttpResponse Calculate(string number1 , string method , string number2)
        {
            decimal firstNumber = decimal.Parse(number1);
            decimal secondNumber = decimal.Parse(number2);
            decimal result = 0;
            string[] methodsValid = { "+", "-", "/", "*" };

            if (!methodsValid.Contains(method))
            {
                this.ViewData["result"] = "Invalid Sign";
                this.ViewData["display"] = "block";
                return this.FileViewResponse(@"calculate\calc");
            }

            switch (method)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    result = firstNumber / secondNumber;
                    break;
            }

            this.ViewData["result"] = result.ToString();
            this.ViewData["display"] = "block";
            return this.FileViewResponse(@"calculate\calc");
        }
    }
}
