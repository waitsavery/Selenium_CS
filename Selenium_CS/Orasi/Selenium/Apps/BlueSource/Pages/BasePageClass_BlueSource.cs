using System;
//Orasi Namespaces
using Orasi.Selenium.Core.PageClass;
using Orasi.Selenium.Core.TestClass;
using Orasi.Utilities;

namespace Orasi.Selenium.Apps.Bluesource.Pages
{
    /// <summary>
    ///     This class inherits generic fields and methods for a web application page class, 
    ///     and defines fields and methods specific for the BlueSource application.
    /// </summary>
    public class BasePageClass_BlueSource : BasePageClass
    {
        //Define a properties object to read in data from a properties file
        Properties prop = new Properties();
        //Define the application under test
        string application = "BlueSource";
        //Define th eenvironment under test
        string environment = "stage";

        //Dummy constructor
        public BasePageClass_BlueSource() { }

        /// <summary>
        ///     Constructor that defines page timeouts
        /// </summary>
        /// <param name="testClass">BaseTestClass object that contains the WebDriver</param>
        public BasePageClass_BlueSource(BaseTestClass testClass)
        {
            setDefaultPageLoadTimeout(defaultPageTimeout);
            setImplicitWaitTimeout(defaultImplicitWaitTimeout);
        }

        /// <summary>
        ///     Required overridden method required by BaePageClass
        ///     Retrieve the application URL from the properties file 
        ///     and use the driver to navigate to the application.
        /// </summary>
        public override void launchApplication()
        {
            //Define the URL property based on the application name and the testing environment
            string urlProp = application.ToUpper() + "_URL_" + environment.ToUpper();
            //Get the URL under test
            url = prop.get(urlProp, "1");
            //Navigate to the application
            driver.Url = url;
        }
    }
}
