using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _02.SocialNetwork.Models
{
    partial class User
    {
        private bool CheckLowerLetter(string value)
        {
            foreach (char letter in value)
            {
                if (letter.ToString() == letter.ToString().ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckUpperLetter(string value)
        {
            foreach (char letter in value)
            {
                if (letter.ToString() == letter.ToString().ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDigit(string value)
        {
            bool hasADigit = value.Any(c => char.IsDigit(c));
            return hasADigit;
        }

        private bool CheckSpecialSymbol(string value)
        {
            char[] symbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };

            foreach (char letter in value)
            {
                if (symbols.Contains(letter))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckEmailValidation(string value)
        {
            string regularExpressionString = @"([a-zA-Z0-9][a-zA-Z_\-.]*[a-zA-Z0-9])@([a-zA-Z-]+\.[a-zA-Z-]+(\.[a-zA-Z-]+)*)\b";
            Regex regex = new Regex(regularExpressionString);

            if (!regex.IsMatch(email))
            {
                return false;
            }

            return true;

        }
    }
}
