using System;
//Selenium
using OpenQA.Selenium;

namespace Orasi.Selenium.Core.PageClass
{
    public interface BasePageClassInterface : ElementInterface
    {
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
        Boolean syncPresent(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver);

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
        Boolean syncPresent(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout);

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
        Boolean syncPresent(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout,
                Boolean returnError);

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
        Boolean syncVisible(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver);

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
        Boolean syncVisible(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout);

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
        Boolean syncVisible(Tuple<BasePageClass.locatorType, string> element,
            IWebDriver driver, int timeout, Boolean returnError);

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
        Boolean syncHidden(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver);

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
        Boolean syncHidden(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout);

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
        Boolean syncHidden(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout, Boolean returnError);

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
        Boolean syncEnabled(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver);

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
        Boolean syncEnabled(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout);

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
        Boolean syncEnabled(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout,
                Boolean returnError);

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
        Boolean syncDisabled(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver);

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
        Boolean syncDisabled(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout);

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
        Boolean syncDisabled(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, int timeout,
                Boolean returnError);

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
        Boolean syncTextInElement(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, string text);

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
        Boolean syncTextInElement(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, string text, int timeout);

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
        Boolean syncTextInElement(Tuple<BasePageClass.locatorType, string> element, IWebDriver driver, string text, int timeout, Boolean returnError);

        /// <summary>
        ///     Abstract method to be overriden and defined by an inherriting class.
        ///     Used to launch the application under test.
        /// </summary>
        void launchApplication();
    }
}
