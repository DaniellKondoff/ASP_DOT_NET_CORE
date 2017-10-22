using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMvc.Framework.Helpers
{
    public static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            var firstLetter = char.ToUpper(input.First());
            var rest = input.Substring(1);

            return $"{firstLetter}{rest}";
        }
    }
}
