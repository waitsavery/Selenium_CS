//NUnit
using NUnit.Framework;
//Orasi Namespaces
using Orasi.Selenium.Apps.Bluesource.Pages;
using Orasi.Selenium.Core.TestClass;

namespace Orasi.Selenium.Apps.Bluesource.Tests
{
    /// <summary>
    ///     Test for successful login of the BlueSource application
    /// </summary>
    [TestFixture]
    public class TestLogin : BaseTestClass
    {
        [Test]
        public void BlueSourceLoginTest()
        {
            LoginPage loginPage = new LoginPage(this);
            loginPage.login();
        }
    }
}