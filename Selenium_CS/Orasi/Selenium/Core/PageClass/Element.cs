using System;
using System.Drawing;
//Selenium
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Internal;
//Orasi Namespace
using Orasi.Utilities;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;

namespace Orasi.Selenium.Core.PageClass
{
    /// <summary>
    ///     This class contains all user-defined fields and methods to interact with Selenium-defined WebElements.
    ///     This allows the user more control over test behavior through element interactions.
    /// </summary>
    public class Element : ElementInterface
    {
        //Define a web driver to be used to define interactions with elements
        public IWebDriver driver = null;
        //Define a web element with which to interact
        public static IWebElement we = null;
        //Define an object used ot select values from a dropdown or list box object
        private static SelectElement innerSelect { get; set; }

        /// <summary>
        ///     DateTime object for reporting.
        ///     Holds DateTime data for the moment just prior to an event being executed.
        /// </summary>
        public DateTime dateBefore { get; set; }
        /// <summary>
        ///     DateTime object for reporting.
        ///     Holds DateTime data for the moment just after to an exception occurs.
        /// </summary>
        public DateTime dateAfter { get; set; }
        /// <summary>
        ///     Locator types to be used with the Selenium webdriver, with which to find elements
        /// </summary>
        public enum locatorType { XPATH, CSS, ID, NAME, LINKTEXT, TAGNAME, PARTIALLINKTEXT, CLASSNAME };



        //******************
        //** PAGE TIMEOUT **
        //******************
        /// <summary>
        ///     Time, in seconds, the test will wait for an application page to load
        /// </summary>
        public int defaultPageTimeout = 20;
        /// <summary>
        ///     Time, in seconds, the test will wait to search for an element if it's not immendiately present
        /// </summary>
        public static int defaultImplicitWaitTimeout = 15;

        /// <summary>
        ///     Returns the default timeout used to determine if the page is loaded
        /// </summary>
        /// <returns> Integer seconds to wait </returns>
        public int getDefaultPageLoadTimeout() { return this.defaultPageTimeout; }
        /// <summary>
        ///     Sets the page load timeout
        /// </summary>
        /// <param name="timeout">Amount of time to wait for the page to be loaded</param>
        public void setDefaultPageLoadTimeout(int timeout) { driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds((double)timeout)); }

        /// <summary>
        ///     Returns the amount of time the driver should wait if an element is not immediately present
        /// </summary>
        /// <returns> Integer seconds to wait</returns>
        public int getImplicitWaitTimeout() { return defaultImplicitWaitTimeout; }
        /// <summary>
        ///     Set the amount of time the driver should wait if an element is not immediately present
        /// </summary>
        /// <param name="timeout">Amount of time in seconds to wait</param>
        public void setImplicitWaitTimeout(int timeout) { driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds((double)timeout)); }

        /// <summary>
        ///     This method invokes the driver to find and return a specific element
        /// </summary>
        /// <param name="element"> Defined by an enumerated locator type and a locator string. 
        ///     These will be used by the driver to find and return the element, if it exists. 
        /// </param>
        /// <returns> Selenium IWebElement defined by the locatorTypr and string</returns>
        public IWebElement findElement(Tuple<locatorType, string> element)
        {
            switch (element.Item1)
            {
                //CSS locator
                case locatorType.CSS:
                    we = driver.FindElement(By.CssSelector(element.Item2));
                    break;
                //HTML element ID
                case locatorType.ID:
                    we = driver.FindElement(By.Id(element.Item2));
                    break;
                //XPath
                case locatorType.XPATH:
                    we = driver.FindElement(By.XPath(element.Item2));
                    break;
                //Link text
                case locatorType.LINKTEXT:
                    we = driver.FindElement(By.LinkText(element.Item2));
                    break;
                //HTML element name
                case locatorType.NAME:
                    we = driver.FindElement(By.Name(element.Item2));
                    break;
                //HTML element tag name
                case locatorType.TAGNAME:
                    we = driver.FindElement(By.TagName(element.Item2));
                    break;
                //Partial link text
                case locatorType.PARTIALLINKTEXT:
                    we = driver.FindElement(By.PartialLinkText(element.Item2));
                    break;
                //Class name
                case locatorType.CLASSNAME:
                    we = driver.FindElement(By.ClassName(element.Item2));
                    break;
                default:
                    break;
            }

            return we;
        }

        /// <summary>
        ///     This method invokes the driver to find and return a specific element
        /// </summary>
        /// <param name="element"> Defined by an enumerated locator type and a locator string. 
        ///     These will be used by the driver to find and return the element, if it exists. 
        /// </param>
        /// <returns> Selenium IWebElement defined by the locatorTypr and string</returns>
        public IWebElement findElement(Tuple<locatorType, string> element, IWebDriver driver)
        {
            this.driver = driver;
            return findElement(element);
        }



        /// <summary>
        ///     This method invokes a method to search for an element and determine if it is found on the current page by testing for a null value.
        /// </summary>
        /// <param name="element"> Defined by an enumerated locator type and a locator string. 
        ///     These will be used by the driver to find and return the element, if it exists. </param>
        /// <returns> Boolean 'true' if the element is found, 'false' otherwise </returns>
        public Boolean elementWired(Tuple<locatorType, string> element)
        {
            return (findElement(element) != null);
        }

        /// <summary>
        ///     This method invokes a method to search for an element and, 
        ///     should the element exist, use the Selenium click to interact with it.
        /// </summary>
        /// <param name="element">The element to click</param>
        public void click(Tuple<locatorType, string> element)
        {
            try
            {
                highlight(element, this.driver);
                findElement(element).Click();
            }
            catch (ElementNotVisibleException enve)
            {
                TestReporter.log(enve.StackTrace);
            }
            catch (StaleElementReferenceException sere)
            {
                TestReporter.log(sere.StackTrace);
            }
        }

        /// <summary>
        ///     Click an object using JavaScript
        /// </summary>
        /// <param name="element">The element to click</param>
        /// <param name="driver">The IWebDriver</param>
        public void jsClick(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Create a JavaScript Executor
            var jsDriver = (IJavaScriptExecutor)driver;
            //Define a string, in JavaScript, to be used by the executor to scroll an element into view on the screen and click it
            string jsScrollAndClick = @"arguments[0].scrollIntoView(true);arguments[0].click();";
            //Execute the JavaScript
            jsDriver.ExecuteScript(jsScrollAndClick, new object[] { findElement(element) });
        }

        /// <summary>
        ///     Highlight an object using JavaScript
        /// </summary>
        /// <param name="element">The element to highlight</param>
        /// <param name="driver">The IWebDriver</param>
        public void highlight(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Create a JavaScript Executor
            var jsDriver = (IJavaScriptExecutor)driver;
            //Define a string, in JavaScript, to be used by the executor to highlight an element by changing the border style
            string jsHighlight = @"arguments[0].style.border='3px solid red';";
            //Execute the JavaScript
            jsDriver.ExecuteScript(jsHighlight, new object[] { findElement(element) });
        }

        /// <summary>
        ///     Highlight an object using JavaScript
        /// </summary>
        /// <param name="element">The element to highlight</param>
        /// <param name="driver">The IWebDriver</param>
        public void highlight(IWebElement element, IWebDriver driver)
        {
            //Create a JavaScript Executor
            var jsDriver = (IJavaScriptExecutor)driver;
            //Define a string, in JavaScript, to be used by the executor to highlight an element by changing the border style
            string jsHighlight = @"arguments[0].style.border='3px solid red';";
            //Execute the JavaScript
            jsDriver.ExecuteScript(jsHighlight, new object[] { element });
        }

        /// <summary>
        ///     Highlight an object using JavaScript
        /// </summary>
        /// <param name="element">The element to highlight</param>
        /// <param name="driver">The IWebDriver</param>
        public void highlight(Tuple<locatorType, string> element)
        {
            //Create a JavaScript Executor
            var jsDriver = (IJavaScriptExecutor)driver;
            //Define a string, in JavaScript, to be used by the executor to highlight an element by changing the border style
            string jsHighlight = @"arguments[0].style.border='3px solid red';";
            //Execute the JavaScript
            jsDriver.ExecuteScript(jsHighlight, new object[] { findElement(element) });
        }

        /// <summary>
        ///     Highlight an object using JavaScript
        /// </summary>
        /// <param name="element">The element to highlight</param>
        /// <param name="driver">The IWebDriver</param>
        public void highlight(IWebElement element)
        {
            //Create a JavaScript Executor
            var jsDriver = (IJavaScriptExecutor)driver;
            //Define a string, in JavaScript, to be used by the executor to highlight an element by changing the border style
            string jsHighlight = @"arguments[0].style.border='3px solid red';";
            //Execute the JavaScript
            jsDriver.ExecuteScript(jsHighlight, new object[] { element });
        }

        /// <summary>
        ///     Scroll an element into view on the screen
        /// </summary>
        /// <param name="element">The element to scroll into view</param>
        /// <param name="driver">The IWebDriver</param>
        public void scrollIntoView(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Create a JavaScript Executor and execute a JavaScript string to scroll an element into view on the screen
            ((IJavaScriptExecutor)driver).ExecuteScript(
                    "arguments[0].scrollIntoView(true);", findElement(element));
        }

        /// <summary>
        ///     Move focus to an element using Selenium Actions
        /// </summary>
        /// <param name="element">The element on which to focus</param>
        /// <param name="driver">The IWebDriver</param>
        public void focus(Tuple<locatorType, string> element, IWebDriver driver)
        {
            new Actions(driver).MoveToElement(findElement(element)).Perform();
        }

        /// <summary>
        ///     Move focus to an element and click it using Selenium Actions
        /// </summary>
        /// <param name="element">The element on which to focus</param>
        /// <param name="driver">The IWebDriver</param>
        public void focusClick(Tuple<locatorType, string> element, IWebDriver driver)
        {
            new Actions(driver).MoveToElement(findElement(element)).Click().Perform();
        }

        /// <summary>
        ///     This method invokes a method to search for an element and, 
        ///     should the element exist, grab any text associated with the element.
        /// </summary>
        /// <param name="element">The element to click</param>
        /// <returns>string text of the element, empty string if no text attribute is found</returns>
        public string getText(Tuple<locatorType, string> element)
        {
            return findElement(element).Text;
        }

        /// <summary>
        ///     Return the Selenium 'By' locator for a given element
        /// </summary>
        /// <param name="element">A user-defined tuple to be used to find an element</param>
        /// <returns>Return the By locator object to reuse</returns>
        public By getElementLocator(Tuple<locatorType, string> element)
        {
            By by = null;
            string locator = "";

            //Decompose the string description of the element to isolate the locator type
            try
            {
                //Find a specific, and consistent, starting place in the string
                //startPosition = we.ToString().LastIndexOf("->") + 3;
                //Extract a substring, which is the locator
                locator = getElementLocatorAsString(element);
                //Determine the By locator
                switch (locator)
                {
                    case "className":
                        by = By.ClassName(getElementIdentifier(element));
                        break;
                    case "cssSelector":
                        by = By.CssSelector(getElementIdentifier(element));
                        break;
                    case "id":
                        by = By.Id(getElementIdentifier(element));
                        break;
                    case "linkText":
                        by = By.LinkText(getElementIdentifier(element));
                        break;
                    case "name":
                        by = By.Name(getElementIdentifier(element));
                        break;
                    case "tagName":
                        by = By.TagName(getElementIdentifier(element));
                        break;
                    case "XPath":
                        by = By.XPath(getElementIdentifier(element));
                        break;
                }
                return by;
            }
            catch (Exception e)
            {
                TestReporter.log(e.StackTrace);
                return null;
            }
        }

        /// <summary>
        ///     Return the Selenium 'By' locator, as a string, for a given element
        /// </summary>
        /// <param name="element">A user-defined tuple to be used to find an element</param>
        /// <returns>Return the By locator, as a string, to reuse</returns>
        public string getElementLocatorAsString(Tuple<locatorType, string> element)
        {
            //Use the tuple to find the element
            we = findElement(element);
            int startPosition = 0;
            string locator = "";
            //Find a specific, and consistent, starting place in the string
            startPosition = we.ToString().LastIndexOf("->") + 3;
            //Extract a substring, which is the locator
            locator = (we.ToString().Substring(startPosition,
                    we.ToString().LastIndexOf(":"))).Trim();

            return locator;
        }

        /// <summary>
        ///     Returns the element locator information as a concatenated string
        /// </summary>
        /// <param name="element">Element for which to get the locator info</param>
        /// <returns>string By type and locator string</returns>
        public string getElementLocatorInfo(IWebElement element)
        {
            return element.Item1.ToString() + " = " + element.Item2;
        }

        /// <summary>
        ///     Returns the element identifier as a concatenated string
        /// </summary>
        /// <param name="element">Element for which to get the locator info</param>
        /// <returns>string element identifier</returns>
        public string getElementIdentifier(Tuple<locatorType, string> element)
        {
            //Use the tuple to find the element
            we = findElement(element);
            string locator = "";
            int startPosition = 0;
            //Find a specific, and consistent, starting place in the string
            startPosition = we.ToString().LastIndexOf(": ") + 2;
            //Extract a substring, which is the identifier
            locator = we.ToString().Substring(startPosition,
                    we.ToString().LastIndexOf("]"));

            return locator.Trim();
        }

        /// <summary>
        ///     Returns the relative x-y location on the screen of an element
        /// </summary>
        /// <param name="element">The element for which to get the location</param>
        /// <returns>x-y point screen coordinates</returns>
        public Point getElementLocation(Tuple<locatorType, string> element)
        {
            Point location = findElement(element).Location;
            return location;
        }

        /// <summary>
        ///     Determine if an element is present in the HTML DOM over a given period of time
        /// </summary>
        /// <param name="driver">The IWebDriver</param>
        /// <param name="locator">By locator with which to search the DOM for the element</param>
        /// <returns>Boolean true if the element is present, false otherwise</returns>
        public Boolean webElementPresent(IWebDriver driver, By locator)
        {
            //Define a new WebDriverWait object, defined in part by the driver, which will be used to wait for a condition to be met
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0));

            try
            {
                //Determine if all elements defined by a locator are present in the DOM
                return (wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator)) != null);
            }
            catch (Exception e)
            {
                TestReporter.log(e.StackTrace);
                return false;
            }

        }

        /// <summary>
        ///     Use IWebDriver Wait to determine if object is visible on the screen or not
        /// </summary>
        /// <param name="element">The element for which to search</param>
        /// <param name="driver">The IWebDriver</param>
        /// <returns>Boolean true if the element is visible on the screen, false otherwise</returns>
        public Boolean webElementVisible(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Find the element
            we = findElement(element);
            try
            {
                //Grab the element's location
                Point location = getElementLocation(element);
                //Grab the element's size
                Size size = we.Size;
                //If the x and y locations are greater than zero or the height and width are greater than zero, then the object is deemed visible on the screen
                if ((location.X > 0 & location.Y > 0)
                        | (size.Height > 0 & size.Width > 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebDriverException wde)
            {
                TestReporter.log(wde.StackTrace);
                return false;
            }
        }

        /// <summary>
        ///     Use IWebDriver Wait to determine if object is enabled on the screen or not
        /// </summary>
        /// <param name="element">The element for which to search</param>
        /// <param name="driver">The IWebDriver</param>
        /// <returns>Boolean true if the element is enabled, false otherwise</returns>
        public Boolean webElementEnabled(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define a new WebDriverWait object, defined in part by the driver, which will be used to wait for a condition to be met
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0));
            string stackTrace = null;
            Boolean enabled = false;

            try
            {
                //Determine if an element is clickable
                return (wait.Until(ExpectedConditions.ElementToBeClickable(findElement(element))) != null);
            }
            catch (TimeoutException e)
            {
                stackTrace = e.StackTrace;
            }
            catch (NoSuchElementException nsee)
            {
                stackTrace = nsee.StackTrace;
            }
            catch (StaleElementReferenceException sere)
            {
                stackTrace = sere.StackTrace;
            }
            TestReporter.log(stackTrace);

            return enabled;
        }

        /// <summary>
        ///     Use IWebDriver Wait to determine if object contains the expected text
        /// </summary>
        /// <param name="element">The element for which to search</param>
        /// <param name="driver">The IWebDriver</param>
        /// <param name="text">Text for which to search</param>
        /// <returns>Boolean true if the text is present in the element, false otherwise</returns>
        public Boolean webElementTextPresent(Tuple<locatorType, string> element, IWebDriver driver,
                string text)
        {
            //Define a new WebDriverWait object, defined in part by the driver, which will be used to wait for a condition to be met
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(0));
            Boolean present = false;
            try
            {
                //Determine if the text is present in the actual element
                if (wait.Until(ExpectedConditions.TextToBePresentInElement(findElement(element), text)))
                {
                    present = true;
                }
                //Determine if the text is present in the element attribute 'Value'
                else if (wait.Until(ExpectedConditions.TextToBePresentInElementValue(findElement(element), text)))
                {
                    present = true;
                }
                else
                {
                    present = false;
                }
            }
            catch (Exception ex)
            {
                //If an the element is not found, the element is stale or a timeout occurs...
                if (ex is NoSuchElementException || ex is StaleElementReferenceException || ex is TimeoutException)
                {
                    try
                    {
                        //Determine if the text is present in the element attribute 'Value'
                        if (wait.Until(ExpectedConditions.TextToBePresentInElementValue(findElement(element), text)))
                        {
                            present = true;
                        }
                        else
                        {
                            present = false;
                        }
                    }
                    catch (Exception exc)
                    {
                        //If an the element is not found, the element is stale or a timeout occurs, the element is deemed not present
                        if (exc is NoSuchElementException || exc is StaleElementReferenceException || exc is TimeoutException)
                        {
                            present = false;
                        }
                    }
                }
            }

            return present;
        }

        //****************************************************
        //****************************************************
        //****************************************************
        //**************  TEXTBOX INTERACTIONS ***************
        //****************************************************
        //****************************************************
        //****************************************************

        /// <summary>
        ///     Locates the textbox and clears any text that is currently present
        /// </summary>
        /// <param name="element">The textbox to clear</param>
        public void clear(Tuple<locatorType, string> element)
        {
            TestReporter.log("<i>Clearing the contents of [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ]");
            try
            {
                //Clear the contents of the element
                findElement(element).Clear();
            }
            catch (StaleElementReferenceException sere)
            {
                TestReporter.log(sere.StackTrace);
            }
        }

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys
        /// </summary>
        /// <param name="element">The textbox to set the text</param>
        /// <param name="value">string to which to set the textbox text</param>
        public void set(Tuple<locatorType, string> element, string value)
        {
            TestReporter.log("<i>Setting the text of [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to [ " + value + "].");
            //Find the element and set the text of the element
            try
            {
                findElement(element).SendKeys(value);
            }
            catch (InvalidElementStateException iese)
            {
                TestReporter.log(iese.StackTrace);
            }
            catch (ElementNotVisibleException enve)
            {
                TestReporter.log(enve.StackTrace);
            }
            catch (StaleElementReferenceException sere)
            {
                TestReporter.log(sere.StackTrace);
            }
        }

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys then validates that the correct value is set.
        ///     The test will fail if the expected text (case sensitive) is not found.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void setValidate(Tuple<locatorType, string> element, string value)
        {
            TestReporter.log("<i>Setting the text of [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to [ " + value + "].");
            //Grab the element
            we = findElement(element);

            //Set the text of the element
            try
            {
                we.SendKeys(value);
            }
            catch (InvalidElementStateException iese)
            {
                TestReporter.log(iese.StackTrace);
            }
            catch (ElementNotVisibleException enve)
            {
                TestReporter.log(enve.StackTrace);
            }
            catch (StaleElementReferenceException sere)
            {
                TestReporter.log(sere.StackTrace);
            }
            //Recapture the element, given the text change
            we = findElement(element);
            //Verify that the element text is that which was intended to be entered
            TestReporter.assertTrue(we.Text.Equals(value), "<i>Validating the text of [ <b>@FindBy: "
                    + getElementLocatorInfo(element)
                    + "</b> ] to [ " + value + "].");
        }

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys.
        ///     "Control-A" selects any and all existing text, then the desired value is set.
        ///     The final action is to send the Tab key, thereby invoking any underlying
        ///     JavaScript.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void safeSet(Tuple<locatorType, string> element, string value)
        {
            TestReporter.log("<i>Safe setting the text of [ <b>@FindBy: "
                       + getElementLocatorInfo(element)
                       + "</b> ] to [ " + value + "].");
            //Grab the element
            we = findElement(element);
            //Click the element to move the focus to the element
            we.Click();
            //Highlight any/all text in the textbox
            we.SendKeys(Keys.Control + "a");
            //Send the string, thereby overwriting any/all preexisting text
            we.SendKeys(value);
            //Send the tab key to move focus from the object and trigger any underlying JavaScript
            we.SendKeys(Keys.Tab);
        }

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys.
        ///     The desired value should be a 64 bit encoded string.
        ///     The encoded string should be generated by Orasi.Selenium.Utilities.Base64
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void setSecure(Tuple<locatorType, string> element, string value)
        {
            TestReporter.log("<i>Secure setting the text of [ <b>@FindBy: "
                       + getElementLocatorInfo(element));
            //Grab the element
            we = findElement(element);
            //Decode the value and send the keys to the element
            we.SendKeys(Base64.Base64Decode(value));
        }

        //****************************************************
        //****************************************************
        //****************************************************
        //**************  LISTBOX INTERACTIONS ***************
        //****************************************************
        //****************************************************
        //****************************************************

        /// <summary>
        ///     Wraps Selenium's method. Selects a list/dropdown box option by text value
        /// </summary>
        /// <param name="element">Element from which to select an option</param>
        /// <param name="text">Text option to select from the element</param>
        public void select(Tuple<locatorType, string> element, string text)
        {
            //Grab the element and use it to define a Selenium SelectElement
            try
            {
                innerSelect = new SelectElement(findElement(element));
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }
            //Grab the options for the SelectElement
            IList<string> options = getOptionsAsStrings(element);

            //Determine if the text is an empty string; if so, skip attempting to select an option
            if (!(text.Length == 0))
            {
                try
                {
                    //Determine if the text is found in the list of options
                    if (options.IndexOf(text) > 0)
                    {
                        //Select the value
                        innerSelect.SelectByText(text);
                        //If the text is not a valid option, compile a list of valid options and output them
                    }
                    else
                    {
                        string optionList = "";
                        //Compile the list of options
                        foreach (string option in options)
                        {
                            optionList += option + " | ";
                        }
                        //Output the list of options
                        TestReporter.interfaceLog(" The value of <b>[ " + text + "</b> ] was not found in Listbox [  <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]. Acceptable values are " + optionList + " ]");
                        throw new NoSuchElementException("The value of [ " + text + " ] was not found in Listbox [  @FindBy: " + getElementLocatorInfo(element) + " ]. Acceptable values are " + optionList);
                    }
                }
                catch (NoSuchElementException nsee)
                {
                    TestReporter.log(nsee.StackTrace);
                }
            }
            else
            {
                TestReporter.interfaceLog("Skipping input to Textbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b> ]");
            }
        }


        /// <summary>
        ///     Wraps Selenium's method. Unselects all options in the element
        /// </summary>
        /// <param name="element">Element from which to deselect all options</param>
        public void deselectAll(Tuple<locatorType, string> element)
        {
            //Grab the select element
            try
            {
                innerSelect = new SelectElement(findElement(element));
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }
            //Deselect all options
            innerSelect.DeselectAll();
        }

        /// <summary>
        ///     Wraps Selenium's method. Returns all options, as WebElements, for a select element
        /// </summary>
        /// <param name="element">Element for which to get all options</param>
        /// <returns>List of options as WebElements</returns>
        public IList<IWebElement> getOptionsAsWebElements(Tuple<locatorType, string> element)
        {
            //Grab the select element
            try
            {
                innerSelect = new SelectElement(findElement(element));
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }
            return innerSelect.Options;
        }

        /// <summary>
        ///     Wraps Selenium's method. Returns all options, as Strings, for a select element
        /// </summary>
        /// <param name="element">Element for which to get all options</param>
        /// <returns>string list of options</returns>
        public IList<string> getOptionsAsStrings(Tuple<locatorType, string> element)
        {
            //Define a list to hold the options
            List<string> strOptions = new List<string>();

            try
            {
                //Grab the SelectElement
                innerSelect = new SelectElement(findElement(element));
                //Grab the list of options as web elements
                IList<IWebElement> eleOptions = innerSelect.Options;

                //Add each option, as a string, to the list of options
                foreach (IWebElement ele in eleOptions)
                {
                    strOptions.Add(ele.Text);
                }
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }

            return strOptions;
        }

        /// <summary>
        ///     Wraps Selenium's method. Deselects options from a selectable element
        /// </summary>
        /// <param name="element">Element from which to deselect options</param>
        /// <param name="text">Text to deselect</param>
        public void deselectByVisibleText(Tuple<locatorType, string> element, string text)
        {
            try
            {
                //Grab the SelectElement
                innerSelect = new SelectElement(findElement(element));
                //Deselect the option defined by the text
                innerSelect.DeselectByText(text);
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }
        }

        /// <summary>
        ///     Wraps Selenium's method.  Returns the selected option.
        /// </summary>
        /// <param name="element">Element from which to get the first selected option</param>
        /// <returns>IWebElement first selected option</returns>
        public IWebElement getFirstSelectedOption(Tuple<locatorType, string> element)
        {
            try
            {
                //Grab the SelectElement
                innerSelect = new SelectElement(findElement(element));
                //Grab the selected item within the select element
                we = innerSelect.SelectedOption;
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }
            catch (NoSuchElementException nsee)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] does not exist.\n\n"
                    + nsee.StackTrace);
            }

            return we;
        }

        /// <summary>
        ///     Wraps Selenium method. Determine if an element is selected.
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Boolean true if selected, false otherwise</returns>
        public Boolean isSelected(Tuple<locatorType, string> element)
        {
            Boolean isSelected = false;
            try
            {
                //Grab the SelectElement
                innerSelect = new SelectElement(findElement(element));
                isSelected = ((IWebElement)innerSelect).Selected;
            }
            catch (UnexpectedTagNameException utne)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] is not a select-type element.\n\n"
                    + utne.StackTrace);
            }
            catch (StaleElementReferenceException sere)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] refers to a stale element.\n\n"
                    + sere.StackTrace);
            }
            return isSelected;
        }

        //*****************************************************
        //*****************************************************
        //*****************************************************
        //**************  CHECKBOX INTERACTIONS ***************
        //*****************************************************
        //*****************************************************
        //*****************************************************

        /// <summary>
        ///     Toggle the value of a checkbox
        /// </summary>
        /// <param name="element">Element to toggle</param>
        public void toggle(Tuple<locatorType, string> element)
        {
            //Use the Selenium-click to check the checkbox
            click(element);
        }

        /// <summary>
        ///     Use JavaScript to toggle a checkbox
        /// </summary>
        /// <param name="driver">Current IWebDriver</param>
        /// <param name="element">Element to toggle</param>
        public void jsToggle(IWebDriver driver, Tuple<locatorType, string> element)
        {
            //Create a JavaScript Executor
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            //Execute the JavaScript for the element
            executor.ExecuteScript("arguments[0].click();", findElement(element));
        }

        /// <summary>
        ///     Check a checkbox
        /// </summary>
        /// <param name="element">Element to check</param>
        public void check(Tuple<locatorType, string> element)
        {
            //If the element is not checked...
            if (!isChecked(element))
            {
                //Toggle the element
                try
                {
                    toggle(element);
                }
                catch (SystemException se)
                {
                    TestReporter.interfaceLog(" Checking the Checkbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]", true);
                    throw se;
                }
                TestReporter.interfaceLog(" Checking the Checkbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]");
            }
        }
        /// <summary>
        ///     Uncheck a checkbox
        /// </summary>
        /// <param name="element">Element to uncheck</param>
        public void uncheck(Tuple<locatorType, string> element)
        {
            //If the element is checked...
            if (isChecked(element))
            {
                try
                {
                    //Toggle the element
                    toggle(element);
                }
                catch (SystemException se)
                {
                    TestReporter.interfaceLog(" Unchecking the Checkbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]", true);
                    throw se;
                }

                TestReporter.interfaceLog(" Unchecking the Checkbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>]");

            }
        }

        /// <summary>
        ///     Determine if an element is checked/selected
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Boolean true if checked, false otherwise</returns>
        public Boolean isChecked(Tuple<locatorType, string> element)
        {
            Boolean isChecked = false;
            try
            {
                isChecked = findElement(element).Selected;
            }
            catch (StaleElementReferenceException sere)
            {
                TestReporter.log("The element defined by locatoryType ["
                    + element.Item1.ToString() + "] and identifier ["
                    + element.Item2 + "] refers to a stale element.\n\n"
                    + sere.StackTrace);
            }
            return isChecked;
        }

        /// <summary>
        ///     Check a checkbox and validate it is checked
        /// </summary>
        /// <param name="driver">Current IWebDriver</param>
        /// <param name="element">Element to check</param>
        public void checkValidate(IWebDriver driver, Tuple<locatorType, string> element)
        {
            //Check the checkbox
            check(element);
            //If the element is not checked...
            if (!isChecked(element))
            {
                TestReporter.log(" Checkbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>] was not checked successfully.");
                throw new SystemException("Checkbox [ @FindBy: " + getElementLocatorInfo(element) + " ] was not checked successfully.");
            }
            else
            {
                TestReporter.log("VALIDATED the Checkbox was <b> CHECKED </b> successfully.");
            }
        }

        /// <summary>
        ///     Uncheck a checkbox and validate it is unchecked
        /// </summary>
        /// <param name="driver">Current IWebDriver</param>
        /// <param name="element">Element to uncheck</param>
        public void uncheckValidate(IWebDriver driver, Tuple<locatorType, string> element)
        {
            //Uncheck the checkbox
            uncheck(element);
            //If the element is checked...
            if (isChecked(element))
            {
                TestReporter.log(" Checkbox [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b>] was not checked successfully.");
                throw new SystemException("Checkbox [ @FindBy: " + getElementLocatorInfo(element) + " ] was not checked successfully.");
            }
            else
            {
                TestReporter.log("VALIDATED the Checkbox was <b> UNCHECKED </b> successfully.");
            }
        }
    }
}
