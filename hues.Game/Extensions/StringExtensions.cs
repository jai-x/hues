using System;

namespace hues.Game.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string str, int max)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length <= max)
                return str;

            return str.Substring(0, max) + "…";
        }

        public static bool ToBoolean(this string str) => Convert.ToBoolean(str);
    }
}
