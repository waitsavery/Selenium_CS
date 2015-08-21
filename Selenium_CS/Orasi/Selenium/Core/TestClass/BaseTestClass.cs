using System;
using System.Collections;
//NUnit
using NUnit.Framework;
//Orasi Namespaces
using Orasi.Utilities;

namespace Orasi.Selenium.Core.TestClass
{
    /// <summary>
    ///     Create a base class to be used for tests. 
    ///     Contains elements and methods designed to be implemented at the test level, 
    ///     or in support of test execution.
    /// </summary>
    /// <author>Waightstill W Avery</author>
    /// <version>06/22/2015 - original</version>
    public class BaseTestClass : WebDriverSetup, BaseTestClassInterface
    {
        //Define a dictionary that will define all test context
        public static IDictionary context;
        //Define a TestContext object that will contain all test context
        TestContext testContext;
        //Define a BaseTestClass to be used locally
        BaseTestClass baseTestClass = null;

        /// <summary>
        ///     Time, in seconds, the test will wait when executing JavaScript asynchronously
        /// </summary>
        private int defaultTestTimeout = 30;

        //Dummy constructor
        public BaseTestClass() { }

        /// <summary>
        ///     Constructor that creates the driver and sets the defaul test timeout
        /// </summary>
        /// <param name="browserType">Browser under test</param>
        /// <param name="browserVersion">Browser version under test</param>
        /// <param name="operatingSystem">Operating system under test</param>
        /// <param name="environment">Environment in which to test</param>
        /// <param name="runLocation">Physical test execution environment: local or remote</param>
        public BaseTestClass(string browserType, string browserVersion,
            string operatingSystem, string environment, string runLocation)
        {
            createDriver(browserType, browserVersion, operatingSystem, environment, runLocation);
            setDefaultTestTimeout(defaultTestTimeout);
        }

        //******************
        //** TEST TIMEOUT **
        //******************

        /// <summary>
        ///     Returns the default test timeout
        /// </summary>
        /// <returns>Integer default test timeout</returns>
        public int getDefaultTestTimeout() { return defaultTestTimeout; }
        /// <summary>
        ///     Sets the default test timeout
        /// </summary>
        /// <param name="timeout">Time to which to set the default test timeout</param>
        public void setDefaultTestTimeout(int timeout) { getDriver().Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds((double)timeout)); }

        //*******************
        //** NUnit Methods **
        //*******************

        /// <summary>
        ///     Define a SetUp method to be used with the NUnit framework.
        ///     This method will be executed prior to test execution.
        ///     Any test that inherits the BaseTestClass class will have access to this method.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            //Define the test context which will contain test name and status, for example
            this.testContext = new TestContext(context);
            //Instantiate the BaseTestClass which will create the driver
            baseTestClass = new BaseTestClass("chrome", "", "", "stage", "local");
            //Set the driver for this instance of BaseTestClass
            setDriver(baseTestClass.getDriver());
            //Set the option to either print to console or not
            TestReporter.setPrintToConsole(true);
        }

        /// <summary>
        ///     Define a TearDown method to be used with the NUnit framework.
        ///     This method will be executed after test execution.
        ///     Any test that inherits the BaseTestClass will have access to this method.
        ///     Given the NUnit design, this method will be executed even if an exception occurs during the test.
        ///     This make this an ideal location for driver disposal.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            //Take a screenshot if a test failed. Ideally this will capture the state of the application at the moment of failure.
            try
            {
                if (TestContext.CurrentContext.Result.Status.ToString().ToLower().Equals("failed"))
                {
                    Screenshots.takeScreenShot(TestContext.CurrentContext, getDriver());
                }
            }
            //Catch any system exception and output the stacktrace
            catch (SystemException se)
            {
                Console.WriteLine(se.StackTrace);
            }
            //Even if an exception occurs, dispose of the driver and all associated browsers
            finally
            {
                if (getDriver() != null)
                    getDriver().Dispose();
            }
        }

        /// <summary>
        ///     Returns the current instance of BaseTestClass
        /// </summary>
        /// <returns>An instance of BaseTestClass</returns>
        public BaseTestClass getBaseTestClass()
        {
            return this.baseTestClass;
        }
    }
}
