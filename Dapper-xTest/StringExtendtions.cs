using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dapper_xTest
{
    public static class StringExtendtions
    {
        private static readonly Regex LowerUnderscore = new Regex(@"[\p{Ll}\p{Lu}0-9]+(?=_?)");
        private static readonly Regex PascalCase = new Regex(@"(\p{Lu}+(?=$|\p{Lu}[\p{Ll}0-9])|\p{Lu}?[\p{Ll}0-9]+)");

        static StringExtendtions() { }
        public static string LowerUnderscoreNaming(this string origin)
        {
            return PascalCase.Match(origin).Value.ToLower();
        }
        public static string PascalCaseNaming(this string origin)
        {
            var match = LowerUnderscore.Match(origin);
            return match.Value[0].ToString().ToUpper() + match.Value.Substring(1);
        }
    }
}

