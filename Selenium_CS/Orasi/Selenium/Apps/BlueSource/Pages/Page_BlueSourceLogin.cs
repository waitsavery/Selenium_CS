using System;
//Orasi Namespaces
using Orasi.Selenium.Core.PageClass;
using Orasi.Selenium.Core.TestClass;
using Orasi.Utilities;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;

namespace Orasi.Selenium.Apps.Bluesource.Pages
{
    /// <summary>
    ///     This class inherits fields and methods from the BlueSource base page class and 
    ///     defines fields and methods to specifically interact with the BlueSource Login page.
    /// </summary>
    public class LoginPage : BasePageClass_BlueSource
    {
        //*************************
        //*** Login Page Fields ***
        //*************************

        //Load the properties file as it will be needed for login
        private Properties prop = new Properties();
        PageLoaded pageLoaded = null;

        //***************************
        //*** Login Page Elements ***
        //***************************

        /// <summary>
        ///     Add all desired elements for this page into the element dictionary
        /// </summary>
        private void addElements()
        {
            //Username textbox
            elementDictionary.Add("txtUsername", new Tuple<locatorType, string>(locatorType.ID, "employee_username"));
            //Password textbox
            elementDictionary.Add("txtPassword", new Tuple<locatorType, string>(locatorType.ID, "employee_password"));
            //Login button
            elementDictionary.Add("btnLogin", new Tuple<locatorType, string>(locatorType.NAME, "commit"));
            //All-content header element
            elementDictionary.Add("eleHomePageAllContentHeader", new Tuple<locatorType, string>(locatorType.XPATH, "//*[@id=\"all-content\"]/h1"));
            //Logout link
            elementDictionary.Add("lnkLogout", new Tuple<locatorType, string>(locatorType.XPATH, "/html/body/header/div/nav/ul/li[8]/a"));
            //Content header element
            elementDictionary.Add("eleLoginPageContentHeader", new Tuple<locatorType, string>(locatorType.XPATH, "//*[@id=\"content\"]/h1"));
        }

        //***********************
        //*** Build Page Area ***
        //***********************
        /// <summary>
        ///     Constructor that defines the driver for an instance of this page, 
        ///     defines the elements for this page, and launches the application.
        /// </summary>
        /// <param name="testClass"></param>
        public LoginPage(BaseTestClass testClass)
        {
            driver = testClass.getDriver();
            addElements();
            pageLoaded = new PageLoaded(this.driver);
            launchApplication();
        }

        //*******************************
        //*** Login Page Interactions ***
        //*******************************

        /// <summary>
        ///     Logs into BlueSource by entering a username and password, 
        ///     clicking the login button and verifying that the expected next page is loaded.
        /// </summary>
        public void login()
        {
            enterUsername();
            enterPassword();
            clickLogin();
            TestReporter.assertTrue(verifySucessfulLogin(), "Verify successful login.");
        }

        /// <summary>
        ///     Decode the username and enter it into the textbox
        /// </summary>
        private void enterUsername()
        {
            setSecure(elementDictionary["txtUsername"], prop.get("BLUESOURCE_USERNAME_ADMIN", "1"));
        }

        /// <summary>
        ///     Decode the password and enter it into the textbox
        /// </summary>
        private void enterPassword()
        {
            setSecure(elementDictionary["txtPassword"], prop.get("BLUESOURCE_PASSWORD_ADMIN", "1"));
        }

        /// <summary>
        ///     Clicks the login button
        /// </summary>
        private void clickLogin()
        {
            click(elementDictionary["btnLogin"]);
            //Ensure the login button is no longer visible after clicking
            syncHidden(elementDictionary["btnLogin"], driver);
        }

        /// <summary>
        ///     Verifies successful login
        /// </summary>
        /// <returns>Boolean true if successful, false otherwise</returns>
        private Boolean verifySucessfulLogin()
        {
            PageLoaded pl = new PageLoaded();
            pl.isDomComplete(driver);
            Boolean success = false;
            //If the page header contains "Welcome", then the application is deemed successfully logged into
            if (getText(elementDictionary["eleHomePageAllContentHeader"]).Contains("Welcome"))
            {
                success = true;
            }

            return success;
        }

        /// <summary>
        ///     Logs out of BlueSource by clicking the logout button 
        ///     and verifying that the expected next page is displayed
        /// </summary>
        public void logout()
        {
            clickLogout();
            TestReporter.assertTrue(verifySucessfulLogout(), "Verify successful logout.");
        }

        /// <summary>
        ///     Clicks the logout button
        /// </summary>
        private void clickLogout()
        {
            click(elementDictionary["lnkLogout"]);
        }

        /// <summary>
        ///     Verifies that the application was successfully logged out of
        /// </summary>
        /// <returns>Boolean true if successful, false otherwise</returns>
        private Boolean verifySucessfulLogout()
        {
            Boolean success = false;

            pageLoaded.isDomComplete(driver);

            //If the login page header contains "Login", then the application is deemed successfully logged out of
            if (getText(elementDictionary["eleLoginPageContentHeader"]).Contains("Login"))
            {
                success = true;
            }

            return success;
        }
    }
}
