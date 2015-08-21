using System;
using System.Linq;
using System.Text;

namespace Orasi.Utilities
{
    /// <summary>
    ///     This class is used to define methods which produce string values of a random nature
    /// </summary>
    public static class Randomness
    {
        /// <summary>
        ///     Generates a random 36 character alphanumeric string.
        ///     A typical use for this type of string is message IDs used in web service calls.
        ///     This helps identify a test's footprints in database logs.
        /// </summary>
        /// <returns>Alphanumeric message ID</returns>
        public static string generateMessageId()
        {
            return randomAlphaNumeric(8) + "-" + randomAlphaNumeric(6) + "-"
                    + randomAlphaNumeric(6) + "-" + randomAlphaNumeric(6) + "-"
                    + randomAlphaNumeric(10);
        }

        /// <summary>
        ///     Generate the date and time in a format seen commonly with web service calls
        /// </summary>
        /// <returns>Current date and time</returns>
        public static string generateCurrentDatetime()
        {
            string currDateTime = "";

            try
            {
                //Account for an approximate millisecond accuracy
                currDateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.StackTrace);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.StackTrace);
            }

            return currDateTime;
        }

        /// <summary>
        ///     Overloaded method - Generate the date and time in a user-defined format
        /// </summary>
        /// <param name="format">Pattern to generate the date</param>
        /// <returns>Current date and time</returns>
        public static string generateCurrentDatetime(string format)
        {
            string currDateTime = "";

            try
            {
                //Account for an approximate millisecond accuracy
                currDateTime = string.Format("{0:" + format + " HH:mm:ss.fff}", DateTime.Now);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.StackTrace);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.StackTrace);
            }

            return currDateTime;
        }

        /// <summary>
        ///     Generate the date and time, for a future date, in a format seen commonly with web service calls
        /// </summary>
        /// <param name="daysOut">Number of days out from the current date to generate a date</param>
        /// <returns>Future date and time</returns>
        public static string generateCurrentDatetime(int daysOut)
        {
            string currDateTime = "";

            try
            {
                //Account for an approximate millisecond accuracy
                currDateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.Now.AddDays(daysOut));
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.StackTrace);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.StackTrace);
            }

            return currDateTime;
        }

        /// <summary>
        ///     Generate the date and time, for a future date, in a user-defined format
        /// </summary>
        /// <param name="daysOut">Number of days out from the current date to generate a date</param>
        /// <param name="format">Pattern to generate the date</param>
        /// <returns>Future date and time</returns>
        public static string generateCurrentDatetime(int daysOut, string format)
        {
            string currDateTime = "";

            try
            {
                //Account for an approximate millisecond accuracy
                currDateTime = string.Format("{0:" + format + " HH:mm:ss.fff}", DateTime.Now.AddDays(daysOut));
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.StackTrace);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.StackTrace);
            }

            return currDateTime;
        }

        /// <summary>
        ///     Generate the date and time in a format seen commonly with SOAP messages
        /// </summary>
        /// <returns>Current date and time</returns>
        public static string generateCurrentXMLDatetime()
        {
            string currXmlDateTime = "";
            string xmlDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            string xmlTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
            currXmlDateTime = xmlDate + "T" + xmlTime;

            return currXmlDateTime;
        }

        /// <summary>
        ///     Generate the date and time in a user-defined format
        /// </summary>
        /// <param name="format">Pattern to generate the date</param>
        /// <returns>Current date and time</returns>
        public static string generateCurrentXMLDatetime(string format)
        {
            string currXmlDateTime = "";
            string xmlDate = string.Format("{0:" + format + "}", DateTime.Now);
            string xmlTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
            currXmlDateTime = xmlDate + "T" + xmlTime;

            return currXmlDateTime;
        }

        /// <summary>
        ///     Generate the date and time, for a future date, in a format seen commonly with SOAP messages
        /// </summary>
        /// <param name="daysOut">Number of days out from the current date to generate a date</param>
        /// <returns>Future date and time</returns>
        public static string generateCurrentXMLDatetime(int daysOut)
        {
            string currXmlDateTime = "";
            string xmlDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(daysOut));
            string xmlTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
            currXmlDateTime = xmlDate + "T" + xmlTime;

            return currXmlDateTime;
        }

        /// <summary>
        ///     Generate the date and time, for a future date, in a user-defined format
        /// </summary>
        /// <param name="daysOut">Number of days out from the current date to generate a date</param>
        /// <param name="format">Pattern to generate the date</param>
        /// <returns>Future date and time</returns>
        public static string generateCurrentXMLDatetime(int daysOut, string format)
        {
            string currXmlDateTime = "";
            string xmlDate = string.Format("{0:" + format + "}", DateTime.Now.AddDays(daysOut));
            string xmlTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
            currXmlDateTime = xmlDate + "T" + xmlTime;

            return currXmlDateTime;
        }

        /// <summary>
        ///     Generate the date in a format seen commonly with SOAP messages
        /// </summary>
        /// <returns>Current date</returns>
        public static string generateCurrentXMLDate()
        {
            string xmlDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            return xmlDate;
        }

        /// <summary>
        ///     Generate the date in a user-defined format
        /// </summary>
        /// <param name="format">Pattern to generate the date</param>
        /// <returns>Current date</returns>
        public static string generateCurrentXMLDate(string format)
        {
            string xmlDate = string.Format("{0:" + format + "}", DateTime.Now);

            return xmlDate;
        }

        /// <summary>
        ///     Generate the date, for a future date, in a format seen commonly with SOAP message
        /// </summary>
        /// <param name="daysOut">Number of days out from the current date to generate a date</param>
        /// <returns>Future date</returns>
        public static string generateCurrentXMLDate(int daysOut)
        {
            string xmlDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(daysOut));

            return xmlDate;
        }

        /// <summary>
        ///     Generate the date, for a future date, in a user-defined format
        /// </summary>
        /// <param name="daysOut">Number of days out from the current date to generate a date</param>
        /// <param name="format">Pattern to generate the date</param>
        /// <returns>Future date</returns>
        public static string generateCurrentXMLDate(int daysOut, string format)
        {
            string xmlDate = string.Format("{0:" + format + "}", DateTime.Now.AddDays(daysOut));

            return xmlDate;
        }

        /// <summary>
        ///     Generate a rendom 32 bit integer
        ///     Initially tries to use the current date as the upper range.
        ///     On exception, the max integer value is used as the upper range.
        /// </summary>
        /// <returns>Random 32 bit integer as a string</returns>
        public static string randomNumber()
        {
            string ranNum = "";
            string myString = string.Format("{0:MMddHHmmss}", DateTime.Now);
            int maxValue = 0;
            Random ran = new Random();

            try
            {
                maxValue = int.Parse(myString);
            }
            catch (OverflowException)
            {
                maxValue = Int32.MaxValue;
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.StackTrace);
            }

            try
            {
                ranNum = ran.Next(maxValue).ToString();
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                Console.WriteLine(aoore.StackTrace);
            }

            return ranNum;
        }

        /// <summary>
        ///     Generates a random mumeric string with a user-defined number of characters
        /// </summary>
        /// <param name="length">Number of characters desired in the string</param>
        /// <returns>Random numeric string</returns>
        public static string randomNumber(int length)
        {
            Random ran = new Random();

            StringBuilder builder = new StringBuilder();
            string ch;
            for (int i = 0; i < length; i++)
            {
                ch = ran.Next(1, 10).ToString();
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Generate a random alphabetic string with a user-defined number of characters
        /// </summary>
        /// <param name="length">Number of characters desired in the string</param>
        /// <returns>Random alpahabetic string</returns>
        public static string randomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        /// <summary>
        ///     Generate a random alphanumeric string with a user-defined number of characters
        /// </summary>
        /// <param name="length">Number of characters desired in the string</param>
        /// <returns>Random alphanumeric string</returns>
        public static string randomAlphaNumeric(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
