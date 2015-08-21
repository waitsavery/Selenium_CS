//NUnit
using NUnit.Framework;
//Orasi Namespaces
using Orasi.Selenium.Apps.Bluesource.Pages;
using Orasi.Selenium.Core.TestClass;

namespace Orasi.Selenium.Apps.Bluesource.Tests
{
    /// <summary>
    ///     Login to the BlueSource application and test for successful logout
    /// </summary>
    [TestFixture]
    public class TestLogout : BaseTestClass
    {
        [Test]
        public void BlueSourceLogoutTest()
        {
            LoginPage loginPage = new LoginPage(this);
            loginPage.login();
            loginPage.logout();
        }
    }
}
