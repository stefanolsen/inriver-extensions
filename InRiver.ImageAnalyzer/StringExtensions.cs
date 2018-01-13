using System;
using System.Text.RegularExpressions;

namespace InRiver.ImageAnalyzer
{
    public static class StringExtensions
    {
        private static readonly Regex CleanCvlKeyRegex = new Regex(@"[^\p{L}\p{Nd}]+", RegexOptions.Compiled);

        public static string RemoveSpecialCharacters(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            string cleanText = CleanCvlKeyRegex.Replace(text, String.Empty);

            return cleanText;
        }
    }
}
