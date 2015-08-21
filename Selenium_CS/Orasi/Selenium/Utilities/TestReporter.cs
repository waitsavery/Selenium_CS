using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace Orasi.Utilities
{
    /// <summary>
    ///     Contains members and methods to facilitate custom test reporting
    /// </summary>
    public class TestReporter
    {
        //Define a member that can turn on and off the option to print to the console
        private static Boolean printToConsole = false;

        /// <summary>
        ///     Generate the current date/time stamp
        /// </summary>
        /// <returns>String surrent date/time</returns>
        private static string getTimestamp()
        {
            return Randomness.generateCurrentDatetime() + " :: ";
        }

        /// <summary>
        ///     Trim any HTML formatting from a string
        /// </summary>
        /// <param name="log"></param>
        /// <returns>String with any HTML formatting removed</returns>
        private static string trimHtml(string log)
        {
            return Regex.Replace(log, "<[^>]*>", "");
        }

        /// <summary>
        ///     Set the value that determines if anything is to be output to the console
        /// </summary>
        /// <param name="printToConsole">Boolean true to print to console, false otherwise</param>
        public static void setPrintToConsole(Boolean printToConsole)
        {
            TestReporter.printToConsole = printToConsole;
        }

        /// <summary>
        ///     Get the value that determines if anything is to be output to the console
        /// </summary>
        /// <returns>Boolean true to print to console, false otherwise</returns>
        public static Boolean getPrintToConsole()
        {
            return printToConsole;
        }

        /// <summary>
        ///     Logs a Selenium step 
        /// </summary>
        /// <param name="step">String description of the step</param>
        public static void logStep(string step)
        {
            if (getPrintToConsole()) Console.WriteLine(step);
        }

        /// <summary>
        ///     Logs a test scenario
        /// </summary>
        /// <param name="scenario">String description of the scenario</param>
        public static void logScenario(string scenario)
        {
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + trimHtml(scenario));
        }

        /// <summary>
        ///     Logs an element interaction
        /// </summary>
        /// <param name="message">String description of the element interaction</param>
        public static void interfaceLog(string message)
        {
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + trimHtml(message.Trim()));
        }

        /// <summary>
        ///     Logs an element interaction
        /// </summary>
        /// <param name="message">String description of the element interaction</param>
        /// <param name="failed">Boolean true if a step failed, false otherwise</param>
        public static void interfaceLog(string message, Boolean failed)
        {
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + trimHtml(message.Trim()));
        }

        /// <summary>
        ///     Generic method to log any message
        /// </summary>
        /// <param name="message">String description to be logged</param>
        public static void log(string message)
        {
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + trimHtml(message));
        }

        /// <summary>
        ///     Generic method to log any message
        /// </summary>
        /// <param name="message">String description to be logged</param>
        /// <param name="stdOut">Boolean true if the message is to output to the console, false otherwise</param>
        public static void log(string message, Boolean stdOut)
        {
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + trimHtml(message));
        }

        /// <summary>
        ///     Report a message containing XML formatting
        /// </summary>
        /// <param name="message">Message containing XML formatting</param>
        public static void logSoapXml(string message)
        {
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + message);
        }

        /// <summary>
        ///     Wrapper method assertion to determine if a condition is true
        /// </summary>
        /// <param name="condition">Boolean condition to verify if it's true</param>
        /// <param name="description">Description to report if the condition is false</param>
        public static void assertTrue(Boolean condition, string description)
        {
            try
            {
                Assert.IsTrue(condition, description);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert True Failed- " + trimHtml(description));
                Assert.Fail(description);
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert True Passed- " + trimHtml(description));
        }

        /// <summary>
        ///     Wrapper method assertion to determine if a condition is false
        /// </summary>
        /// <param name="condition">Boolean condition to verify if it's false</param>
        /// <param name="description">Description to report if the condition is true</param>
        public static void assertFalse(Boolean condition, string description)
        {
            try
            {
                Assert.IsFalse(condition, description);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert False Failed- " + trimHtml(description));
                Assert.Fail(description);
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert False - " + trimHtml(description));
        }

        /// <summary>
        ///     Wrapper method assertion to determine if a condition is equal
        /// </summary>
        /// <param name="condition">Boolean condition to verify if it's equal</param>
        /// <param name="description">Description to report if the condition is not equal</param>
        public static void assertEquals(Boolean condition, string description)
        {
            try
            {
                Assert.Equals(condition, description);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Equals Failed- " + trimHtml(description));
                Assert.Fail(description);
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Equals - " + trimHtml(description));

        }

        /// <summary>
        ///     Wrapper method assertion to determine if a condition is not equal
        /// </summary>
        /// <param name="condition">Boolean condition to verify if it's not equal</param>
        /// <param name="description">Description to report if the condition is equal</param>
        public static void assertNotEquals(Boolean condition, string description)
        {
            try
            {
                Assert.AreNotEqual(condition, description);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Not Equals Failed- " + trimHtml(description));
                Assert.Fail(description);
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Not Equals - " + trimHtml(description));
        }

        /// <summary>
        ///     Wrapper method assertion to determine if an integer value is greater than zero
        /// </summary>
        /// <param name="condition">Integer to determine if it's greater than zero</param>
        public static void assertGreaterThanZero(int value)
        {
            try
            {
                Assert.IsTrue(value > 0);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Greater Than Zero Failed- Assert " + value + " is greater than zero");
                Assert.Fail("Assert " + value + " is greater than zero");
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Greater Than Zero - Assert " + value + " is greater than zero");
        }

        /// <summary>
        ///     Wrapper method assertion to determine if a floating point value is greater than zero
        /// </summary>
        /// <param name="condition">Floating point value to determine if it's greater than zero</param>
        /// <param name="description">Description to report if it's greater than zero</param>
        public static void assertGreaterThanZero(float value)
        {
            try
            {
                Assert.IsTrue(value > 0);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Greater Than Zero Failed- Assert " + value + " is greater than zero");
                Assert.Fail("Assert " + value + " is greater than zero");
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Greater Than Zero - Assert " + value + " is greater than zero");
        }

        /// <summary>
        ///     Wrapper method assertion to determine if a double value is greater than zero
        /// </summary>
        /// <param name="condition">Double value to determine if it's greater than zero</param>
        /// <param name="description">Description to report if the condition is not greater than zero</param>
        public static void assertGreaterThanZero(double value)
        {
            try
            {
                Assert.IsTrue(value > 0);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Greater Than Zero Failed- Assert " + value + " is greater than zero");
                Assert.Fail("Assert " + value + " is greater than zero");
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Greater Than Zero - Assert " + value + " is greater than zero");
        }

        /// <summary>
        ///     Wrapper method assertion to determine if an object is null
        /// </summary>
        /// <param name="condition">Object to determine if it's null</param>
        /// <param name="description">Description to report if the object is not null</param>
        public static void assertNull(Boolean condition, string description)
        {
            try
            {
                Assert.IsNull(condition, description);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Null Failed- " + trimHtml(description));
                Assert.Fail(description);
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Null - " + trimHtml(description));
        }

        /// <summary>
        ///     Wrapper method assertion to determine if an object is not null
        /// </summary>
        /// <param name="condition">Object to determine if it's not null</param>
        /// <param name="description">Description to report if the object is null</param>
        public static void assertNotNull(Boolean condition, string description)
        {
            try
            {
                Assert.IsNotNull(condition, description);
            }
            catch (AssertionException)
            {
                if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Not Null Failed- " + trimHtml(description));
                Assert.Fail(description);
            }
            if (getPrintToConsole()) Console.WriteLine(getTimestamp() + "Assert Not Null - " + trimHtml(description));
        }
    }
}
