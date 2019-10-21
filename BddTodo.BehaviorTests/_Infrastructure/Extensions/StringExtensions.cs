using System;
using System.Globalization;

namespace BddTodo.Tests._Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source.IndexOf(value, comparisonType) >= 0;
        }

        public static string ToTitleCase(this string source)
        {
            var myTi = new CultureInfo("en-US", false).TextInfo;

            //ToTitleCase doesn't work on all caps words, so make sure word is lower case
            var loweredWord = source.ToLower();
            return myTi.ToTitleCase(loweredWord);
        }

        public static string RemoveSpaces(this string source)
        {
            return string.IsNullOrEmpty(source) ? string.Empty : source.Replace(" ", "");
        }

        public static string TrimString(this string source, int charsToKeep, bool addEllipses = false)
        {
            // return empty string if source is null or empty
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            // return source if the length is less than or equal characters to keep
            if (source.Length <= charsToKeep)
            {
                return source;
            }

            // trim the string
            var trimmedString = source.Substring(0, charsToKeep);

            // add ellipses (if necessary)
            if (addEllipses)
            {
                trimmedString = string.Format("{0}...", trimmedString);
            }

            return trimmedString;
        }

        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotNullOrEmpty(this string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotNullOrWhiteSpace(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
