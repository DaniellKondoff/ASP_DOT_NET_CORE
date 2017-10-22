using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.App.Views.Users
{
    public class Register
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h3>Register new user</h3>");
            sb.AppendLine(@"<form action=""register"" method=""POST"">");
            sb.AppendLine(@"Username: <input type=""text"" name=""Username""/>");
            sb.AppendLine(@"Password: <input type=""password"" name=""Password""/>");
            sb.AppendLine(@"<input type=""submit"" value=""Register""/>");
            sb.AppendLine("</form>");

            return sb.ToString();
        }
    }
}
