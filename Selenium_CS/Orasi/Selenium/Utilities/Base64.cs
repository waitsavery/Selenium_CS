using System;

namespace Orasi.Utilities
{
    /// <summary>
    ///     Class designed to perform simple base64 encoding and decoding
    /// </summary>
    public class Base64
    {
        /// <summary>
        ///     Encode a given string using Base64 encoding
        /// </summary>
        /// <param name="plainText">Text to encode</param>
        /// <returns>string encoded text</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        ///     Decode a given string using Base64 decoding
        /// </summary>
        /// <param name="base64EncodedData">Test to decode</param>
        /// <returns>string decoded text</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
