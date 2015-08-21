//System Namespace
using System;
using System.IO;
//Selenium Namespace
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
//NUnit Namespaces
using NUnit.Framework;
//Orasi Namespaces
using Orasi.Utilities;

namespace Orasi.Selenium.Core.TestClass
{
    /// <summary>
    ///     Create a class to create a webdriver.
    ///     This class, and subsequent drivers, are inherited into the base test and page classes
    /// </summary>
    public class WebDriverSetup
    {
        //Define the Base Test Class properties
        private IWebDriver driver { get; set; }
        private ICapabilities caps = null;
        private string browserType { get; set; }
        private string browserVersion { get; set; }
        private string operatingSystem { get; set; }
        private string environment { get; set; }
        private string runLocation { get; set; }

        //Load the properties file as it will be used for the URL under test
        private Properties prop = new Properties();

        //Grab the current working directory
        string cwd = Directory.GetCurrentDirectory();

        //Dummy constructor
        public WebDriverSetup() { }

        /// <summary>
        ///     Overloaded constructor used to create the driver
        /// </summary>
        /// <param name="browserType">browser under test</param>
        /// <param name="browserVersion">browser version under test</param>
        /// <param name="operatingSystem">operating system under test</param>
        /// <param name="environment">test environment</param>
        /// <param name="runLocation">determines if the test is to be executed remotely or locally</param>
        public WebDriverSetup(string browserType, string browserVersion,
            string operatingSystem, string environment, string runLocation)
        {
            createDriver(browserType, browserVersion, operatingSystem, environment, runLocation);
        }

        /// <summary>
        ///     Method to create the driver based on string values 
        ///     that define the configuration to be tested
        /// </summary>
        /// <param name="browserType">browser under test</param>
        /// <param name="browserVersion">browser version under test</param>
        /// <param name="operatingSystem">operating system under test</param>
        /// <param name="environment">test environment</param>
        /// <param name="runLocation">determines if the test is to be executed remotely or locally</param>
        public void createDriver(string browserType, string browserVersion,
            string operatingSystem, string environment, string runLocation)
        {
            //Set the browser under test
            setBrowserType(browserType);
            //Set the browser version under test
            setBrowserVersion(browserVersion);
            //Set the operating system under test
            setOperatingSystem(operatingSystem);
            //Set the environment under test
            setEnvironment(environment);
            //Set the run location (e.g. local or remote)
            setRunLocation(runLocation);

            //**********************************************
            //**********************************************
            //***************             ******************
            //*************** RUN LOCALLY ******************
            //***************             ******************
            //**********************************************
            //**********************************************
            if (runLocation.ToLower().Equals("local"))
            {
                //Determine the type of browser to test
                switch (browserType.ToLower())
                {
                    //Test the chrome browser
                    case "chrome":
                        //driver = new ChromeDriver(@"C:\selenium-dotnet-2.43.1\drivers");
                        driver = new ChromeDriver(@"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CS\Selenium_CS\Orasi\Drivers");
                        break;
                    //Test the firefox browser
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;
                    default:
                        Assert.IsTrue(driver != null, "The browser type " + browserType + " is not supported by this Selenium setup.");
                        break;
                }
            }
            //**********************************************
            //**********************************************
            //***************              *****************
            //*************** RUN REMOTELY *****************
            //***************              *****************
            //**********************************************
            //**********************************************
            else
            {
                Uri uri = new Uri(prop.get("BLUESOURCE_URL_" + getEnvironment().ToUpper(), "1"));
                driver = new ScreenShotRemoteWebDriver(uri, caps);
                Assert.IsTrue(driver != null, "The browser type " + browserType + " is not supported by this Selenium setup.");
            }
            //Set the driver - this will be inherited by the test class
            setDriver(driver);

            //Maximize the browser window
            driver.Manage().Window.Maximize();
            //Delete all cookies
            driver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        ///     Used to tear down the driver. 
        ///     This would render the driver useless to further testing.
        /// </summary>
        public void tearDown()
        {
            getDriver().Close();
            getDriver().Quit();
            getDriver().Dispose();
        }

        //************
        //** DRIVER **
        //************
        /// <summary>
        ///     Retrieve the current driver
        /// </summary>
        /// <returns>Current WebDriver</returns>
        public IWebDriver getDriver() { return this.driver; }
        /// <summary>
        ///     Set the current driver
        /// </summary>
        /// <param name="newDriver">Driver to be used for testing</param>
        public void setDriver(IWebDriver newDriver) { this.driver = newDriver; }

        //***********************
        //** DRIVER PARAMETERS **
        //***********************

        /// <summary>
        ///     Retrieve the browser under test
        /// </summary>
        /// <returns>string browser under test</returns>
        public string getBrowserType() { return this.browserType; }
        /// <summary>
        ///     Set the browser under test
        /// </summary>
        /// <param name="browserType">Browser under test</param>
        public void setBrowserType(string browserType) { this.browserType = browserType; }

        /// <summary>
        ///     Retrieve the browser version under test
        /// </summary>
        /// <returns>Browser version under test</returns>
        public string getBrowserVersion() { return this.browserVersion; }
        /// <summary>
        ///     Set the browser version under test
        /// </summary>
        /// <param name="browserVersion">Browser version under test</param>
        public void setBrowserVersion(string browserVersion) { this.browserVersion = browserVersion; }

        /// <summary>
        ///     Retrieve the operating system under test
        /// </summary>
        /// <returns>Operating system under test</returns>
        public string getOperatingSystem() { return this.operatingSystem; }
        /// <summary>
        ///     Set the operating system under test
        /// </summary>
        /// <param name="operatingSystem">Operating system under test</param>
        public void setOperatingSystem(string operatingSystem) { this.operatingSystem = operatingSystem; }

        /// <summary>
        ///     Retrieve the testing environment
        /// </summary>
        /// <returns>Testing environment</returns>
        public string getEnvironment() { return this.environment; }
        /// <summary>
        ///     Set the testing environment
        /// </summary>
        /// <param name="environment">Testing environment</param>
        public void setEnvironment(string environment) { this.environment = environment; }

        /// <summary>
        ///     Retrieve the run location
        /// </summary>
        /// <returns>Run location</returns>
        public string getRunLocation() { return this.runLocation; }
        /// <summary>
        ///     Set the run location
        /// </summary>
        /// <param name="runLocation">Run location</param>
        public void setRunLocation(string runLocation) { this.runLocation = runLocation; }
    }

    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        {
        }

        public Screenshot GetScreenshots()
        {
            // Get the screenshot as base64. 
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();
            // ... and convert it. 
            return new Screenshot(base64);
        }
    }
}
