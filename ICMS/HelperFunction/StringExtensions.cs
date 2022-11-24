using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICMS.HelperFunction
{
    public static class StringExtensions
    {
        public static bool ContainsWord(this string word, string otherword)
        {
            int currentIndex = 0;

            foreach (var character in otherword)
            {
                currentIndex = CultureInfo.InvariantCulture.CompareInfo.IndexOf(word, character, currentIndex, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreNonSpace);

                if (currentIndex == -1)
                {
                    return false;
                }

            }

            return true;
        }

        public static bool ContainsWithOptionStringComparison(this string str, string substring, StringComparison comp)
        {
            if (substring == null)
            {
                return false;
                //throw new ArgumentNullException("substring",  "substring cannot be null.");
            }

            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                         "comp");

            return str.IndexOf(substring, comp) >= 0;
        }

        public static bool CompareWithString(this string word, string otherword)
        {

            //return (String.Compare(word, otherword, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols)  == 0);

            if (String.Compare(word, otherword, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string RemoveAllWhiteSpaces(string str)
        {
            str = str.Trim();
            String newStr = Regex.Replace(str, @"\s", "");
            return newStr;
        }
    }
}
