using System;
using System.Text.RegularExpressions;

namespace Orasi.Utilities
{
    /// <summary>
    ///     Contains members and methods to extend
    /// </summary>
    public class RegEx
    {
        /// <summary>
        ///     Determines if a value matches a regular expression pattern
        /// </summary>
        /// <param name="regex">Regular expression pattern</param>
        /// <param name="value">Value to compare to the regular expression pattern</param>
        /// <param name="caseSensitive">Boolean true if the comparison is to be case-sensitive, false otherwise</param>
        /// <returns></returns>
        public static Boolean match(string regex, string value, Boolean caseSensitive)
        {
            //Define a boolean to hold the result of the comparison
            Boolean isAMatch = false;
            //Define a System.Text.RegularExpressions.Regex object
            Regex rx = null;
            //Define a RegexOptions object with no options set
            RegexOptions options = RegexOptions.None;

            //Determine if the comparison is not to be case-sensitive
            if (!caseSensitive)
            {
                //Add the IgnoreCase option to the RegexOptions object
                options = RegexOptions.IgnoreCase;
            }
            //Instantiate a new Regex object with the predefined pattern and options
            rx = new Regex(regex, options);

            //Get all occurrences in the value that match the pattern
            MatchCollection matches = rx.Matches(value);
            //If there is at least one match...
            if (matches.Count > 0)
            {
                //Grab the first match and determine if it was successful
                Match match = matches[0];
                if (match.Success == true)
                {
                    isAMatch = true;
                }
            }

            return isAMatch;
        }
    }
}
