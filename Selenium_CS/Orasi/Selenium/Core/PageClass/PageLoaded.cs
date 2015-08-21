using System;
using System.Threading;
//Selenium
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Orasi.Selenium.Core.PageClass
{
    /// <summary>
    ///     Several different methods of waiting for a page to finish loading.
    /// </summary>
    /// <author>
    ///     Justin Phlegar
    /// </author>
    /// <version>
    ///     10/16/2014: original (in Java)
    ///     06/23/2015: modified to C#/.NET
    /// </version>
    public class PageLoaded : Element
    {
        //private BaseTestClass btc = new BaseTestClass();
        private static IWebDriver localDriver = null;
        //private Object clazz = null;
        private static int localTimeout = 0;

        public PageLoaded() { }

        public PageLoaded(IWebDriver driver)
        {
            localDriver = driver;
        }

        /// <summary>
        ///     Initialize all elements on the current page as proxies
        /// </summary>
        /// <param name="driver">The WebDriver</param>
        /// <param name="clazz">The Current Page</param>
        public static void initialize(IWebDriver driver, Object clazz)
        {
            //Initialize elements
            PageFactory.InitElements(driver, clazz);
        }

        /// <summary>
        ///     This waits for a specified element on the page to be found on the page by the driver
        ///     Uses the default test time out set by BaseTestClass
        /// </summary>
        /// <param name="clazz">The class calling this method - used so can initialize the page class repeatedly</param>
        /// <param name="element">The element you are waiting to display on the page</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns> Boolean true if the element is loaded, false otherwise </returns>
        /// <author>
        ///     Justin Phlegar
        /// </author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        private Boolean isElementLoaded(IWebDriver driver, Object clazz, Tuple<locatorType, string> element)
        {
            int count = 0;

            //set the timeout for looking for an element to 1 second as we are doing a loop and then refreshing the elements
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));

            //While the element is null, reinitialize all elements
            try
            {
                while (!elementWired(element))
                {
                    if (count == 0)
                    {
                        break;
                    }
                    else
                    {
                        count++;
                        initialize(driver, clazz);
                    }
                }
            }
            catch (NoSuchElementException nsee)
            {
                Console.WriteLine(nsee.StackTrace);
            }
            catch (StaleElementReferenceException sere)
            {
                Console.WriteLine(sere.StackTrace);
            }

            //set the timeout for looking for an element back to the default timeout
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));

            //If a timeout didnt occur, then the element was found
            if (count < getImplicitWaitTimeout())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     This waits for a specified element on the page to be found on the page by the driver
        ///     Uses the default test time out set by BaseTestClass
        /// </summary>
        /// <param name="clazz">The class calling this method - used so can initialize the page class repeatedly</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="element">The element you are waiting to display on the page</param>
        /// <returns> Boolean true if the element is loaded, false otherwise </returns>
        /// <author>
        ///     Justin Phlegar
        /// </author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean isElementLoaded(Object clazz, IWebDriver driver, Tuple<locatorType, string> element)
        {
            return isElementLoaded(driver, clazz, element);
        }

        /// <summary>
        ///     This waits for a specified element on the page to be found on the page by the driver
        ///     Uses a user-defined timeout
        /// </summary>
        /// <param name="clazz">The class calling this method - used so can initialize the page class repeatedly</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="element">The element you are waiting to display on the page</param>
        /// <param name="timeout">The amount of time to wait until the element is loaded</param>
        /// <returns> Boolean true if the element is loaded, false otherwise </returns>
        /// <author>
        ///     Justin Phlegar
        /// </author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean isElementLoaded(Object clazz, IWebDriver driver, Tuple<locatorType, string> element, int timeout)
        {
            localTimeout = getImplicitWaitTimeout();
            setImplicitWaitTimeout(timeout);
            Boolean loaded = isElementLoaded(driver, clazz, element);
            setImplicitWaitTimeout(localTimeout);
            return loaded;
        }

        /// <summary>
        ///     This uses the HTML DOM readyState property to wait until a page is finished loading.  It will wait for
        ///     the ready state to be either 'interactive' or 'complete'.
        /// </summary>
        /// <returns> Boolean true if the DOM is interactive or complete, false otherwise </returns>
        /// <author>
        ///     Jessica Marshall
        /// </author>
        /// <version>
        ///     12/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        private Boolean isDomInteractive()
        {
            int count = 0;
            Object obj = null;

            do
            {
                //this returns a boolean
                obj = ((IJavaScriptExecutor)localDriver).ExecuteScript(
                        "var result = document.readyState; return (result == 'complete' || result == 'interactive');");
                if (count == 0)
                    break;
                else
                {
                    Thread.Sleep(500);
                    count++;
                }
            } while (obj.Equals(false));


            if (count < getImplicitWaitTimeout() * 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     Overload method - This uses the HTML DOM readyState property to wait until a page is finished loading.  
        ///     It will wait for the ready state to be either 'interactive' or 'complete'.
        /// </summary>
        /// <param name="driver">The WebDriver</param>
        /// <returns> Boolean true if the DOM is interactive or complete, false otherwise </returns>
        /// <author>
        ///     Jessica Marshall
        /// </author>
        /// <version>
        ///     12/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean isDomInteractive(IWebDriver driver)
        {
            localDriver = driver;
            return isDomInteractive();
        }

        /// <summary>
        ///     Overload method - This uses the HTML DOM readyState property to wait until a page is finished loading.  
        ///     It will wait for the ready state to be either 'interactive' or 'complete'.
        /// </summary>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">The time to wait for the DOM to becom interactive</param>
        /// <returns> Boolean true if the DOM is interactive or complete, false otherwise </returns>
        /// <author>
        ///     Jessica Marshall
        /// </author>
        /// <version>
        ///     12/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean isDomInteractive(IWebDriver driver, int timeout)
        {
            localDriver = driver;
            localTimeout = getImplicitWaitTimeout();
            setImplicitWaitTimeout(timeout);
            Boolean interactive = isDomInteractive(driver);
            setImplicitWaitTimeout(localTimeout);
            return interactive;
        }

        /// <summary>
        ///     This uses protractor method to wait until a page is ready - notifyWhenNoOutstandingRequests
        /// </summary>
        /// <author>
        ///     Justin Phlegar
        /// </author>
        /// <version>
        ///     10/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public void isAngularComplete(IWebDriver driver)
        {
            try
            {
                Object obj = ((IJavaScriptExecutor)driver).ExecuteAsyncScript("var callback = arguments[arguments.length - 1];" +
                            "angular.element(document.body).injector().get('$browser').notifyWhenNoOutstandingRequests(callback);");
            }
            catch (WebDriverException)
            {
                isDomComplete();
            }
        }

        /// <summary>
        ///     A more strict version of isDomInteractive.  
        ///     This uses the HTML DOM readyState property to wait until a page is finished loading.  
        ///     It will wait for the ready state to be 'complete'.
        /// </summary>
        /// <returns> Boolean true if the DOM is complete, false otherwise </returns>
        /// <author>
        ///     Jessica Marshall
        /// </author>
        /// <version>
        ///     12/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        private Boolean isDomComplete()
        {
            int count = 0;
            Object obj = null;

            do
            {
                //this returns a boolean
                obj = ((IJavaScriptExecutor)localDriver).ExecuteScript(
                        "var result = document.readyState; return (result == 'complete');");
                if (count == getImplicitWaitTimeout())
                    break;
                else
                {
                    Thread.Sleep(500);
                    count++;

                }
            } while (obj.Equals(false));


            if (count < getImplicitWaitTimeout() * 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     A more strict version of isDomInteractive.  
        ///     This uses the HTML DOM readyState property to wait until a page is finished loading.  
        ///     It will wait for the ready state to be 'complete'.
        /// </summary>
        /// <param name="driver">The WebDriver</param>
        /// <returns> Boolean true if the DOM is complete, false otherwise </returns>
        /// <author>
        ///     Jessica Marshall
        /// </author>
        /// <version>
        ///     12/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean isDomComplete(IWebDriver driver)
        {
            localDriver = driver;
            return isDomComplete();
        }

        /// <summary>
        ///     A more strict version of isDomInteractive.  
        ///     This uses the HTML DOM readyState property to wait until a page is finished loading.  
        ///     It will wait for the ready state to be 'complete'.
        /// </summary>
        /// <param name="driver">The WebDriver</param>
        /// <param name="timeout">The amount of time to wait for the DOM to be complete</param>
        /// <returns> Boolean true if the DOM is complete, false otherwise </returns>
        /// <author>
        ///     Jessica Marshall
        /// </author>
        /// <version>
        ///     12/16/2014: original (in Java)
        ///     06/23/2015: modified to C#/.NET
        /// </version>
        public Boolean isDomComplete(IWebDriver driver, int timeout)
        {
            localTimeout = getImplicitWaitTimeout();
            setImplicitWaitTimeout(timeout);
            Boolean complete = isDomComplete(driver);
            setImplicitWaitTimeout(localTimeout);
            return complete;
        }
    }
}
