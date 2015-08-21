using NUnit.Framework;
using OpenQA.Selenium;
using Orasi.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orasi.Selenium.Core.PageClass
{
    public class RadioGroup : Element
    {
        //Define a radio group element; 
        //this is simply a parent node that contains child nodes with the type "radio"
        private IWebElement radioGroup;
        //Define a list of redio buttons belonging to the rasio group
        private IList<IWebElement> radioButtons;
        //Define the number of radio buttons in the radio group
        private int numberOfRadioButtons;
        //Define a list of labels (i.e. a list of labels associated with the radio buttons)
        private IList<string> stringLabels;
        //Define the number of radio button labels
        private int numberOfLabels;
        //Define the number of the current index
        private int currentIndex = -1;
        //Define the currently selected label
        private string selectedLabel = null;
        //Define a tuple used to locate objects
        private Tuple<locatorType, string> element;

        /// <summary>
        ///     Constructor for a radio group element.  
        ///     Sets essential values that the RadioGroup methods may use.
        /// </summary>
        /// <param name="element">Element to be located</param>
        public RadioGroup(Tuple<locatorType, string> element, IWebDriver driver)
        {
            //Define the driver locally
            this.driver = driver;
            //Define the element locally
            this.element = element;
            //Grab the parent element whose child elements have the attribute "type" has value "radio"
            this.radioGroup = findElement(element);
            //Grab a list of the radio buttons
            setRadioButtons(this.driver);
            //Set the number of radio buttons
            this.numberOfRadioButtons = this.radioButtons.Count;
            //Set the values for all available Labels
            setAllLabels();
            //Set the number of Labels
            this.numberOfLabels = stringLabels.Count;
            //Set the initial index
            setCurrentIndexAndLabel();
        }

        /// <summary>
        ///     Grab all of the elements with a tag value "input" and type "radio"
        ///     which will be used to compose the list of radio button WebElements
        /// </summary>
        private void setRadioButtons(IWebDriver driver)
        {
            //Grab the child elements of the radio group that have the tag "input"
            IList<IWebElement> elements = null;
            elements = this.radioGroup.FindElements(By.TagName("input"));
            //Initialize the radioButtons object
            this.radioButtons = new List<IWebElement>();

            //Iterate through all input elements and find all elements of type "radio"
            foreach (IWebElement ele in elements)
            {
                //If they are radio buttons, add them to the list of radio buttons
                if (ele.GetAttribute("type").ToLower().Equals("radio"))
                {
                    //Add each radio button to the list
                    this.radioButtons.Add(ele);
                }
            }
        }

        /// <summary>
        ///     Using a list of radio buttons, grab the element's Label/name
        ///     and add it to a list
        /// </summary>
        private void setAllLabels()
        {
            //Grab the child elements of the radio group that have the tag "label"
            IList<IWebElement> elements = null;
            elements = this.radioGroup.FindElements(By.TagName("label"));
            //Initialize the stringLabels object
            this.stringLabels = new List<string>();
            //Add each label to the list
            foreach (IWebElement element in elements)
            {
                this.stringLabels.Add(element.Text);
            }
        }

        /// <summary>
        ///     Loops through all radio buttons for one that possesses an attribute 
        ///		that indicates the button is selected and use that to define the current 
        ///		index and seleted Label.
        ///     NOTE: Within the method, the field "string[] attributes" is a string array for possible values 
        ///     that could indicate radio button  is selected/checked. This array can be appended with new 
        ///     attributes that indicate an Label is selected/checked.
        /// </summary>
        private void setCurrentIndexAndLabel()
        {
            //Array of strings that can indicate that an element is checked
            string[] attributes = { "checked" };
            int loopCounter = 0;
            int attributeLoopCounter = 0;
            string isChecked = null;

            //Iterate through the list of radio buttons
            for (loopCounter = 0; loopCounter < numberOfRadioButtons; loopCounter++)
            {
                //Iterate through the list of attributes that can be used to indicate if a radio button, if any, is checked
                for (attributeLoopCounter = 0; attributeLoopCounter < attributes.Length; attributeLoopCounter++)
                {
                    //If a radio button is checked...
                    if (!(radioButtons[loopCounter].GetAttribute(attributes[attributeLoopCounter]) == null))
                    {
                        //If the radio button is checked...
                        isChecked = radioButtons[loopCounter].GetAttribute(attributes[attributeLoopCounter]).ToLower();
                        if (isChecked.Equals("true"))
                        {
                            //Set the current index and selected Label
                            this.currentIndex = loopCounter;
                            this.selectedLabel = radioButtons[this.currentIndex].Text;
                            break;
                        }
                    }
                }
                //If an element is found to be checked, then break out of the loop
                if (isChecked != null)
                {
                    break;
                }
            }
        }

        /// <summary>
        ///     Select and check a radio button by index value
        /// </summary>
        /// <param name="index">Index of the radio button to check</param>
        public void selectByIndex(int index)
        {
            //Set the current index and selected Label
            this.currentIndex = index;
            this.selectedLabel = stringLabels[this.currentIndex];

            //Grab the radio button by index and click it
            try
            {
                radioButtons[this.currentIndex].Click();
            }
            catch (ElementNotVisibleException)
            {
                TestReporter.interfaceLog("Select Label <b> [ " + currentIndex + " ] </b> from the radio group [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b> ]", true);
            }
            catch (StaleElementReferenceException)
            {
                TestReporter.interfaceLog("Select Label <b> [ " + currentIndex + " ] </b> from the radio group [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b> ]", true);
            }
        }

        /// <summary>
        ///     Select and check a radio button by Label/name
        /// </summary>
        /// <param name="Label"></param>
        public void selectByLabel(string label)
        {
            //Set the selected Label
            this.selectedLabel = label;

            //Find the index of the radio button that has the user-defined Label
            try
            {
                this.currentIndex = this.stringLabels.IndexOf(label);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                TestReporter.log("An error occurred trying to access a member of the list of Labels.\nSTACK TRACE:\n" + aoore.StackTrace);
            }

            //Grab the radio button and click it
            try
            {
                radioButtons[this.currentIndex].Click();
            }
            catch (ElementNotVisibleException)
            {
                TestReporter.interfaceLog("Select Label <b> [ " + currentIndex + " ] </b> from the radio group [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b> ]", true);
            }
            catch (StaleElementReferenceException)
            {
                TestReporter.interfaceLog("Select Label <b> [ " + currentIndex + " ] </b> from the radio group [ <b>@FindBy: " + getElementLocatorInfo(element) + " </b> ]", true);
            }
        }

        /// <summary>
        ///     Iterate through each radio button in the radio group and click it
        /// </summary>
        public void cycleAllRadioButtons()
        {
            int indexCounter = 0;

            foreach (IWebElement radioButton in radioButtons)
            {
                //Click the next radio buttons
                radioButton.Click();
                //Set the current index
                this.currentIndex = indexCounter;
                //Set the current label
                this.selectedLabel = stringLabels[this.currentIndex];
                Thread.Sleep(500);
            }
        }
    }
}
