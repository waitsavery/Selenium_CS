using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
//NUnit
using NUnit.Framework;
//Selenium
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Orasi.Utilities
{
    /// <summary>
    ///     Takes screenshots of the current state of the application.
    ///     The intent is to have these methods invoked at the moment of test failure.
    /// </summary>
    /// <author>Waightstill W Avery</author>
    /// <version>06/24/2015 - original</version>
    public class Screenshots
    {
        private ImageFormat iFformat = null;
        public string strFormat = null;
        private static Screenshot ss = null;

        //Dummy Constructor
        public Screenshots() { }

        /// <summary>
        ///     Uses the Selenium GetScreenshot method to take a screenshot.
        ///     If the dircetories do not exists in the project structure, then they are created.
        /// </summary>
        /// <param name="testContext">Contains test name</param>
        /// <param name="driver">The driver used during the test</param>
        public static void takeScreenShot(TestContext testContext, Object driver)
        {
            //Define the test name
            string testName = testContext.Test.Name;

            //get current date time with Date() to create unique file name
            string dateTime = string.Format("{0:dd_MMM_yyyy__hh_mm_ssfff}", DateTime.Now);

            //Get the current directory
            string cwd = Directory.GetCurrentDirectory();

            //Define the output directory name
            string dirScreenshots = "\\test-output\\screenshots\\";

            //Define the directory name to house the screenshots for this test
            string dirTestScreenshots = testName + "\\";

            //To identify the system on which the test was executed
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            string hostAddress = "__" + ipAddress.ToString() + "__";

            //Define the screenshot file name using the dateTime string
            string filename = hostAddress + dateTime + ".png";

            //Define the fully qualified directory path
            string path = cwd + dirScreenshots + dirTestScreenshots;

            //Define the fully qualified file path
            string filePath = path + filename;

            if (!Directory.Exists(path))
            {
                DirectoryInfo dirInfo = Directory.CreateDirectory(path);
            }

            if (driver is RemoteWebDriver)
            {
                ss = ((ITakesScreenshot)driver).GetScreenshot();
            }
            //else
            //{
            //    driver = (RemoteWebDriver)driver;
            //    ss = driver.GetScreenshot();                
            //}

            ss.SaveAsFile(filePath, ImageFormat.Png); //use any of the built in image formating
        }

        /// <summary>
        ///     Set the image type based of value in the constants file
        /// </summary>
        private void setImageType()
        {
            string imageType = Constants.getImageType();
            Boolean validType = true;
            switch (imageType.ToLower())
            {
                case "png":
                    iFformat = ImageFormat.Png;
                    strFormat = ".png";
                    break;
                case "jpg":
                case "jpeg":
                    iFformat = ImageFormat.Jpeg;
                    strFormat = ".jpg";
                    break;
                default:
                    validType = false;
                    break;
            }

            Assert.IsTrue(validType, "The image file format [" + imageType + "] is not valid.");
        }
    }
}
