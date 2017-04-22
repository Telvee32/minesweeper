using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Telvee32.Minesweeper.ConsoleUI
{
    public static class Extensions
    {
        /// <summary>
        /// Thanks kmkemp!
        /// </summary>
        /// <returns></returns>
        public static string ToPascalCase(this string s)
        {
            string PascalCaseSingleWord(string str)
            {
                var match = Regex.Match(str, @"^(?<word>\d+|^[a-z]+|[A-Z]+|[A-Z][a-z]+|\d[a-z]+)+$");
                var groups = match.Groups["word"];

                var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                var res = new StringBuilder();
                foreach (var capture in groups.Captures.Cast<Capture>())
                {
                    res.Append(textInfo.ToTitleCase(capture.Value.ToLower()));
                }
                return res.ToString();
            }

            var result = new StringBuilder();
            var nonWordChars = new Regex(@"[^a-zA-Z0-9]+");
            var tokens = nonWordChars.Split(s);
            foreach (var token in tokens)
            {
                result.Append(PascalCaseSingleWord(token));
            }

            return result.ToString();
        }        
    }
}
