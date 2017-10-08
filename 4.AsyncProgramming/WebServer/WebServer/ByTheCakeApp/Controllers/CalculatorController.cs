using System.Collections.Generic;
using System.Linq;
using WebServer.ByTheCakeApp.Infrastructure;
using WebServer.Server.HTTP.Contracts;

namespace WebServer.ByTheCakeApp.Controllers
{
    public class CalculatorController : ControllerBase
    {
        public IHttpResponse Calculate()
        {
            return this.FileViewResponse(@"calculate\calc", new Dictionary<string, string>
            {
                ["display"] = "none"
            });

        }

        public IHttpResponse Calculate(string number1 , string method , string number2)
        {
            decimal firstNumber = decimal.Parse(number1);
            decimal secondNumber = decimal.Parse(number2);
            decimal result = 0;
            string[] methodsValid = { "+", "-", "/", "*" };

            if (!methodsValid.Contains(method))
            {
                return this.FileViewResponse(@"calculate\calc", new Dictionary<string, string>
                {
                    ["result"] = "Invalid Sign",
                    ["display"] = "block"
                });
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

            return this.FileViewResponse(@"calculate\calc", new Dictionary<string, string>
            {
                ["result"] = result.ToString(),
                ["display"] = "block"
            });
        }
    }
}
