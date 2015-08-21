using System;
using System.Collections.Generic;
using System.Threading;
//Selenium
using OpenQA.Selenium;
//Orasi Namespaces
using Orasi.Utilities;
using Orasi.Selenium.Core.TestClass;

namespace Orasi.Selenium.Core.PageClass
{
    /// <summary>
    ///     This abstract class is a base class to be used for application pages. 
    ///     Contains elements and methods designed to be implemented at the page level, 
    ///     or in support of page validations.
    /// </summary>
    public abstract class BasePageClass : Element, BasePageClassInterface
    {
        //Application URL
        public string url;

        //Create a dictionary to contain all elements
        public Dictionary<string, Tuple<locatorType, string>> elementDictionary = new Dictionary<string, Tuple<locatorType, string>>();

        //Dummy constructor
        public BasePageClass() { }

        /// <summary>
        ///     Contains fields and methods to be used by, or in support of, a page class
        /// </summary>
        /// <param name="testClass">Test class containing the driver</param>
        public BasePageClass(BaseTestClass testClass) { }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectPresent to determine if the desired
        ///     element is present in the DOM. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncPresent(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define a field used to determine if the element is present in the DOM
            Boolean found = false;
            //Initialize a loop timeout using the Selenium implicit wait timeout
            double loopTimeout = getImplicitWaitTimeout() * 10;
            //Get the By locator for the element
            By locator = getElementLocator(element);
            //Report the attempt to sync
            TestReporter.log("<i> Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>PRESENT</b> in DOM within [ "
                    + getImplicitWaitTimeout().ToString() + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is present in the DOM, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the locator and driver to determine if the element is present in the DOM
                if (webElementPresent(driver, locator))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not present...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                           + getElementLocatorInfo(element)
                           + " ] is not PRESENT on the page after [ "
                           + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                           + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectPresent to determine if the desired
        ///     element is present in the DOM. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncPresent(Tuple<locatorType, string> element, IWebDriver driver, int timeout)
        {
            //Define a field used to determine if the element is present in the DOM
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Get the By locator for the element
            By locator = getElementLocator(element);
            //Report the attempt to sync
            TestReporter.log("<i> Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>PRESENT</b> in DOM within [ "
                    + timeout.ToString() + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is present in the DOM, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the locator and driver to determine if the element is present in the DOM
                if (webElementPresent(driver, locator))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not present...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                           + getElementLocatorInfo(element)
                           + " ] is not PRESENT on the page after [ "
                           + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                           + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectPresent to determine if the desired
        ///     element is present in the DOM. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="returnError">Boolean to determine whether to fail the test if the element is not found during the timeout</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncPresent(Tuple<locatorType, string> element, IWebDriver driver, int timeout,
                Boolean returnError)
        {
            //Define a field used to determine if the element is present in the DOM
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Get the By locator for the element
            By locator = getElementLocator(element);
            //Report the attempt to sync
            TestReporter.log("<i> Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>PRESENT</b> in DOM within [ " + timeout.ToString()
                    + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is present in the DOM, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the locator and driver to determine if the element is present in the DOM
                if (webElementPresent(driver, locator))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not present and the user wants the test to fail if the element is not present...
            if (!found && returnError)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                           + getElementLocatorInfo(element)
                           + " ] is not PRESENT on the page after [ "
                           + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                           + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectVisible to determine if the desired
        ///     element is on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncVisible(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define a field used to determine if the element is visible on the page
            Boolean found = false;
            //Initialize a loop timeout
            double loopTimeout = getImplicitWaitTimeout() * 10;
            //Report the attempt to sync
            TestReporter.log("<i> Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>VISISBLE</b> within [ "
                    + loopTimeout.ToString() + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is visible on the page, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is visible on the page
                if (webElementVisible(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not visible...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                           + getElementLocatorInfo(element)
                           + " ] is not VISIBLE on the page after [ "
                           + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                           + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectVisible to determine if the desired
        ///     element is on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncVisible(Tuple<locatorType, string> element, IWebDriver driver, int timeout)
        {
            //Define a field used to determine if the element is visible on the page
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i> Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>VISISBLE</b> within [ " + timeout.ToString()
                    + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is visible on the page, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is visible on the page
                if (webElementVisible(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not visible...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not VISIBLE on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectVisible to determine if the desired
        ///     element is on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="returnError">Boolean to determine whether to fail the test if the element is not found during the timeout</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncVisible(Tuple<locatorType, string> element,
            IWebDriver driver, int timeout, Boolean returnError)
        {
            //Define a field used to determine if the element is visible on the page
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                + getElementLocatorInfo(element)
                + "</b> ] to be <b>VISIBLE/<b> within [ " + timeout.ToString()
                + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is visible on the page, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is visible on the page
                if (webElementVisible(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not visible and the user wants the test to fail if the element is not visible...
            if (!found && returnError)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not VISIBLE on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectVisible to determine if the desired
        ///     element is hidden from the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncHidden(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define a field used to determine if the element is hidden on the page
            Boolean found = false;
            //Initialize a loop timeout using the Selenium implicit wait timeout
            long loopTimeout = getImplicitWaitTimeout() * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>HIDDEN</b> within [ "
                    + getImplicitWaitTimeout().ToString() + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is hidden on the page, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is hidden on the page
                if (!webElementVisible(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not hidden...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("<i>Element [<b>@FindBy: "
                        + getElementLocatorInfo(element)
                        + " </b>] is not <b>HIDDEN</b> on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.</i>");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectVisible to determine if the desired
        ///     element is hidden from the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncHidden(Tuple<locatorType, string> element, IWebDriver driver, int timeout)
        {
            //Define a field used to determine if the element is hidden on the page
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            long loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>HIDDEN</b> within [ "
                    + timeout.ToString() + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is hidden on the page, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is hidden on the page
                if (!webElementVisible(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not hidden...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("<i>Element [<b>@FindBy: "
                        + getElementLocatorInfo(element)
                        + " </b>] is not <b>HIDDEN</b> on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.</i>");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectVisible to determine if the desired
        ///     element is hidden from the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="returnError">Boolean to determine whether to fail the test if the element is not found during the timeout</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncHidden(Tuple<locatorType, string> element, IWebDriver driver, int timeout, Boolean returnError)
        {
            //Define a field used to determine if the element is hidden on the page
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            long loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>HIDDEN</b> within [ "
                    + timeout.ToString() + " ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is hidden on the page, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is hidden on the page
                if (!webElementVisible(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not hidden and the user wants the test to fail if the element is not hidden...
            if (!found && returnError)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("<i>Element [<b>@FindBy: "
                        + getElementLocatorInfo(element)
                        + " </b>] is not <b>HIDDEN</b> on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.</i>");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectEnabled to determine if the desired
        ///     element is enabled on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncEnabled(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define a field used to determine if the element is enabled such that you could click it
            Boolean found = false;
            //Initialize a loop timeout using the Selenium implicit wait timeout
            double loopTimeout = getImplicitWaitTimeout() * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>ENABLED</b> within [ <b>"
                    + getImplicitWaitTimeout()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is enabled such that you could click it, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is enabled such that you could click it
                if (webElementEnabled(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not enabled such that you could click it...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not ENABLED on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;

        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectEnabled to determine if the desired
        ///     element is enabled on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncEnabled(Tuple<locatorType, string> element, IWebDriver driver, int timeout)
        {
            //Define a field used to determine if the element is enabled such that you could click it
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>ENABLED</b> within [ <b>" + timeout
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is enabled such that you could click it, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is enabled such that you could click it
                if (webElementEnabled(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }

            }

            //If the element is not enabled such that you could click it...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not ENABLED on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectEnabled to determine if the desired
        ///     element is enabled on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="returnError">Boolean to determine whether to fail the test if the element is not found during the timeout</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncEnabled(Tuple<locatorType, string> element, IWebDriver driver, int timeout,
                Boolean returnError)
        {
            //Define a field used to determine if the element is enabled such that you could click it
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>ENABLED</b> within [ <b>" + timeout
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is enabled such that you could click it, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is enabled such that you could click it
                if (webElementEnabled(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not enabled such that you could click it and 
            //the user wants the test to fail if the element is not enabled such that you could click it...
            if (!found && returnError)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not ENABLED on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectEnabled to determine if the desired
        ///     element is disabled on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncDisabled(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define a field used to determine if the element is enabled such that you could not click it
            Boolean found = false;
            //Initialize a loop timeout using the Selenium implicit wait timeout
            double loopTimeout = getImplicitWaitTimeout() * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>DISABLED</b> within [ <b>"
                    + getImplicitWaitTimeout().ToString()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is enabled such that you could click it, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //Use the element descriptors and driver to determine if the element is enabled such that you could click it
                if (!webElementEnabled(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not disabled such that you could not click it...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not DISABLED on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;

        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectEnabled to determine if the desired
        ///     element is disabled on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncDisabled(Tuple<locatorType, string> element, IWebDriver driver, int timeout)
        {
            //Define a field used to determine if the element is enabled such that you could not click it
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>DISABLED</b> within [ <b>" + timeout.ToString()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is enabled such that you could click it, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //If the element is not disabled such that you could not click it...
                if (!webElementEnabled(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not disabled such that you could not click it...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not DISABLED on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectEnabled to determine if the desired
        ///     element is disabled on the screen. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="returnError">Boolean to determine whether to fail the test if the element is not found during the timeout</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncDisabled(Tuple<locatorType, string> element, IWebDriver driver, int timeout,
                Boolean returnError)
        {
            //Define a field used to determine if the element is enabled such that you could not click it
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be <b>DISABLED</b> within [ <b>" + timeout.ToString()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the element is enabled such that you could click it, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //If the element is not disabled such that you could not click it...
                if (!webElementEnabled(element, driver))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);
                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the element is not disabled such that you could not click it and 
            //the user wants the test to fail if the element is not disabled such that you could not click it...
            if (!found && returnError)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] is not DISABLED on the page after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectText to determine if the desired
        ///     text is present in the desired element. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="text">Text for which to search an element for</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncTextInElement(Tuple<locatorType, string> element, IWebDriver driver, string text)
        {
            //Define a field used to determine if the text in the element, if any, is that which is expected
            Boolean found = false;
            //Initialize a loop timeout using the Selenium implicit wait timeout
            double loopTimeout = getImplicitWaitTimeout() * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to text in element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be displayed within [ <b>"
                    + getImplicitWaitTimeout()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the text in the element, if any, is that which is expected, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //If the element is not disabled such that you could not click it...
                if (webElementTextPresent(element, driver, text))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the text in the element, if any, is that which is expected...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] did not contain the text [ " + text + " ] after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;

        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectText to determine if the desired
        ///     text is present in the desired element. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="text">Text for which to search an element for</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncTextInElement(Tuple<locatorType, string> element, IWebDriver driver, string text, int timeout)
        {
            //Define a field used to determine if the text in the element, if any, is that which is expected
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            double loopTimeout = timeout * 10;
            //Report the attempt to sync
            TestReporter.log("<i>Syncing to text in element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be displayed within [ <b>"
                    + timeout.ToString()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the text in the element, if any, is that which is expected, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //If the element is not disabled such that you could not click it...
                if (webElementTextPresent(element, driver, text))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the text in the element, if any, is that which is expected...
            if (!found)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] did not contain the text [ " + text + " ] after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Used in conjunction with Element.WebObjectText to determine if the desired
        ///     text is present in the desired element. Will loop for the time out listed in
        ///     BaseTestClass.defaultTestTimeout. If object is not present within the time, throw
        ///     an error
        /// </summary>
        /// <param name="element">Element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">Amount of time for which to wait for the element to be present in the DOM</param>
        /// <param name="returnError">Boolean to determine whether to fail the test if the element is not found during the timeout</param>
        /// <param name="text">Text for which to search an element for</param>
        /// <returns>Boolean true is the element is present, false otherwise</returns>
        /// <author>Justin Phlegar</author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean syncTextInElement(Tuple<locatorType, string> element, IWebDriver driver, string text, int timeout, Boolean returnError)
        {
            //Define a field used to determine if the text in the element, if any, is that which is expected
            Boolean found = false;
            //Initialize a loop timeout using the user-defined timeout
            //Report the attempt to sync
            double loopTimeout = timeout * 10;
            //Grab the date/time before attempting the sync
            TestReporter.log("<i>Syncing to text in element [<b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to be displayed within [ <b>" + timeout.ToString()
                    + "</b> ] seconds.</i>");
            //Grab the date/time before attempting the sync
            dateBefore = new DateTime();

            //Loop until the text in the element, if any, is that which is expected, or until the timeout is reached
            for (double seconds = 0; seconds < loopTimeout; seconds += 1)
            {
                //If the element is not disabled such that you could not click it...
                if (webElementTextPresent(element, driver, text))
                {
                    found = true;
                    break;
                }
                //Sleep for a tenth of a second
                try
                {
                    Thread.Sleep(100);

                }
                catch (ArgumentOutOfRangeException aoore)
                {
                    Console.WriteLine(aoore.StackTrace);
                }
            }

            //If the text in the element, if any, is that which is expected and 
            //the user wants the test to fail if the text in the element, if any, is that which is expected...
            if (!found && returnError)
            {
                //Grab the dat/time after attempting the sync
                dateAfter = new DateTime();
                //Throw an exception reporting all element data
                throw new Exception("Element [ @FindBy: "
                        + getElementLocatorInfo(element)
                        + " ] did not contain the text [ " + text + " ] after [ "
                        + (dateAfter.TimeOfDay - dateBefore.TimeOfDay).Seconds.ToString()
                        + " ] seconds.");
            }
            return found;
        }

        /// <summary>
        ///     Abstract method to be overriden and defined by an inherriting class.
        ///     Used to launch the application under test.
        /// </summary>
        public abstract void launchApplication();
    }
}
