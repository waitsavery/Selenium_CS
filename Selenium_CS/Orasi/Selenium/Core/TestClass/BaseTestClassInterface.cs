namespace Orasi.Selenium.Core.TestClass
{
    /// <summary>
    ///     Create an interface class to be used to prototype methods for the BaseTestClass.
    /// </summary>
    /// <author>Waightstill W Avery</author>
    /// <version>06/22/2015 - original</version>
    public interface BaseTestClassInterface
    {
        /// <summary>
        ///     Returns the default test timeout
        /// </summary>
        /// <returns>Integer default test timeout</returns>
        int getDefaultTestTimeout();

        /// <summary>
        ///     Sets the default test timeout
        /// </summary>
        /// <param name="timeout">Time to which to set the default test timeout</param>
        void setDefaultTestTimeout(int timeout);
    }
}
