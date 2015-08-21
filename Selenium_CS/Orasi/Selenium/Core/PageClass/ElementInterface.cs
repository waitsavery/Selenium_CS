using System;
using System.Drawing;
//Selenium
using OpenQA.Selenium;
//Orasi Namespaces
using Orasi.Selenium.Core;
using System.Collections.Generic;

namespace Orasi.Selenium.Core.PageClass
{
    public interface ElementInterface
    {
        /// <summary>
        ///     Returns the default timeout used to determine if the page is loaded
        /// </summary>
        /// <returns> Integer seconds to wait </returns>
        int getDefaultPageLoadTimeout();
        /// <summary>
        ///     Sets the page load timeout
        /// </summary>
        /// <param name="timeout">Amount of time to wait for the page to be loaded</param>
        void setDefaultPageLoadTimeout(int timeout);

        //***********************
        //** PAGE INTERACTIONS **
        //***********************

        /// <summary>
        ///     This method invokes a method to search for an element and, 
        ///     should the element exist, use the Selenium click to interact with it.
        /// </summary>
        /// <param name="element">The element to click</param>
        void click(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Click an object using JavaScript
        /// </summary>
        /// <param name="element">The element to click</param>
        /// <param name="driver">The WebDriver</param>
        void jsClick(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     Highlight an object using JavaScript
        /// </summary>
        /// <param name="element">The element to highlight</param>
        /// <param name="driver">The WebDriver</param>
        void highlight(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     Scroll an element into view on the screen
        /// </summary>
        /// <param name="element">The element to scroll into view</param>
        /// <param name="driver">The WebDriver</param>
        void scrollIntoView(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     Move focus to an element using Selenium Actions
        /// </summary>
        /// <param name="element">The element on which to focus</param>
        /// <param name="driver">The WebDriver</param>
        void focus(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     Move focus to an element and click it using Selenium Actions
        /// </summary>
        /// <param name="element">The element on which to focus</param>
        /// <param name="driver">The WebDriver</param>
        void focusClick(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     This method invokes a method to search for an element and, 
        ///     should the element exist, grab any text associated with the element.
        /// </summary>
        /// <param name="element">The element to click</param>
        /// <returns>string text of the element, empty string if no text attribute is found</returns>
        string getText(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Return the Selenium 'By' locator for a given element
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Return the By locator object to reuse</returns>
        By getElementLocator(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Return the Selenium 'By' locator, as a string, for a given element
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Return the By locator, as a string, to reuse</returns>
        string getElementLocatorAsString(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Returns the element locator information as a concatenated string
        /// </summary>
        /// <param name="element">Element for which to get the locator info</param>
        /// <returns>string By type and locator string</returns>
        string getElementLocatorInfo(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Returns the element locator identifier as a concatenated string
        /// </summary>
        /// <param name="element">Element for which to get the locator info</param>
        /// <returns>string element identifier</returns>
        string getElementIdentifier(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Returns the x-y location on the screen of an element
        /// </summary>
        /// <param name="element">The element for which to get the location</param>
        /// <returns>x-y point screen coordinates</returns>
        Point getElementLocation(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Determine if an element is present in the HTML DOM over a given period of time
        /// </summary>
        /// <param name="driver">The WebDriver</param>
        /// <param name="locator">By locator with which to search the DOM for the element</param>
        /// <returns>Boolean true if the element is present, false otherwise</returns>
        Boolean webElementPresent(IWebDriver driver, By locator);

        /// <summary>
        ///     Use WebDriver Wait to determine if object is visible on the screen or not
        /// </summary>
        /// <param name="element">The element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true if the element is visible on the screen, false otherwise</returns>
        Boolean webElementVisible(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     Use WebDriver Wait to determine if object is enabled on the screen or not
        /// </summary>
        /// <param name="element">The element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <returns>Boolean true if the element is enabled, false otherwise</returns>
        Boolean webElementEnabled(Tuple<Element.locatorType, string> element, IWebDriver driver);

        /// <summary>
        ///     Use WebDriver Wait to determine if object contains the expected text
        /// </summary>
        /// <param name="element">The element for which to search</param>
        /// <param name="driver">The WebDriver</param>
        /// <param name="text">Text for which to search</param>
        /// <returns>Boolean true if the text is present in the element, false otherwise</returns>
        Boolean webElementTextPresent(Tuple<Element.locatorType, string> element, IWebDriver driver, string text);

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
        void clear(Tuple<Element.locatorType, string> element);

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys
        /// </summary>
        /// <param name="element">The textbox to set the text</param>
        /// <param name="value">string to which to set the textbox text</param>
        void set(Tuple<Element.locatorType, string> element, string value);

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys then validates that the correct value is set.
        ///     The test will fail if the expected text (case sensitive) is not found.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        void setValidate(Tuple<Element.locatorType, string> element, string value);

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys.
        ///     "Control-A" selects any and all existing text, then the desired value is set.
        ///     The final action is to send the Tab key, thereby invoking any underlying
        ///     JavaScript.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        void safeSet(Tuple<Element.locatorType, string> element, string value);

        /// <summary>
        ///     Locates the textbox and sets the text using Selenium SendKeys.
        ///     The desired value should be a 64 bit encoded string.
        ///     The encoded string should be generated by Orasi.Selenium.Utilities.Base64
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        void setSecure(Tuple<Element.locatorType, string> element, string value);

        //****************************************************
        //****************************************************
        //****************************************************
        //**************  LISTBOX INTERACTIONS ***************
        //****************************************************
        //****************************************************
        //****************************************************

        void select(Tuple<Element.locatorType, string> element, string text);
        void deselectAll(Tuple<Element.locatorType, string> element);
        IList<IWebElement> getOptionsAsWebElements(Tuple<Element.locatorType, string> element);
        IList<string> getOptionsAsStrings(Tuple<Element.locatorType, string> element);
        void deselectByVisibleText(Tuple<Element.locatorType, string> element, string text);
        IWebElement getFirstSelectedOption(Tuple<Element.locatorType, string> element);
        Boolean isSelected(Tuple<Element.locatorType, string> element);

        //*****************************************************
        //*****************************************************
        //*****************************************************
        //**************  CHECKBOX INTERACTIONS ***************
        //*****************************************************
        //*****************************************************
        //*****************************************************

        void toggle(Tuple<Element.locatorType, string> element);
        void jsToggle(IWebDriver driver, Tuple<Element.locatorType, string> element);
        void check(Tuple<Element.locatorType, string> element);
        void uncheck(Tuple<Element.locatorType, string> element);
        Boolean isChecked(Tuple<Element.locatorType, string> element);
        void checkValidate(IWebDriver driver, Tuple<Element.locatorType, string> element);
        void uncheckValidate(IWebDriver driver, Tuple<Element.locatorType, string> element);
    }
}
