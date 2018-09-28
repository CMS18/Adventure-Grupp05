using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Extension
{
    public static class Extensions
    {
        public static string ToCapital(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            // Makes sure string is lowercase to start with
            string lowerCase = str.ToLower();

            // Convert to char array of the string
            char[] letters = lowerCase.ToCharArray();
            
            // Upper case the first char
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }
    }
}
