using System;

namespace Orasi.Utilities
{
    /// <summary>
    ///     Contains constant values that are intended not to change
    /// </summary>
    public class Constants
    {
        //Image type for screenshots
        protected static string imageType = "png";

        /// <summary>
        ///     Return the image type to be used for screenshots
        /// </summary>
        /// <returns>string image type</returns>
        public static string getImageType()
        {
            return imageType;
        }
    }
}
