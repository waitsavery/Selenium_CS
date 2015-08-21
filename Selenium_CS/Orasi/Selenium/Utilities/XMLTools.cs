using System;
using System.Xml;
using System.Xml.XPath;

namespace Orasi.Utilities
{
    /// <summary>
    ///     Contains fields and methods to manipulate XML objects, be
    /// </summary>
    public class XMLTools
    {
        /// <summary>
        ///     Convert a string to an XmlDocument and set the value of a node in the XML document
        /// </summary>
        /// <param name="doc">XML document in string format</param>
        /// <param name="xpath">XPath of the desired node</param>
        /// <param name="value">Innertext to set for the node</param>
        /// <returns>XML document with the innertext of the node changed</returns>
        public XmlDocument setXmlNodeValueByPath(string doc, string xpath, string value)
        {
            //Create an XML document and load a string xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(doc);

            //Find a node by xpath and set the innertext
            try
            {
                xmlDoc.SelectSingleNode(xpath).InnerText = value;
            }
            catch (XPathException xpe)
            {
                TestReporter.log("An error exists either with the XPath [" + xpath + "] or the XML [" + doc + ".");
                TestReporter.log(xpe.StackTrace);
            }

            return xmlDoc;
        }

        /// <summary>
        ///     Set the value of a node in a XML document
        /// </summary>
        /// <param name="doc">XML document in XmlDocument format</param>
        /// <param name="xpath">XPath of the desired node</param>
        /// <param name="value">Innertext to set for the node</param>
        /// <returns>XML document with the innertext of the node changed</returns>
        public XmlDocument setXmlNodeValueByXpath(XmlDocument doc, string xpath, string value)
        {
            //Find a node by xpath and set the innertext
            try
            {
                doc.SelectSingleNode(xpath).InnerText = value;
                TestReporter.log(xpath + "  ::  " + value);
            }
            catch (XPathException xpe)
            {
                TestReporter.log("An error exisits either with the XPath [" + xpath + "] or the XML [" + ConvertXmlToString(doc) + ".");
                TestReporter.log(xpe.StackTrace);
            }

            return doc;
        }

        /// <summary>
        ///     Convert a string to an XmlDocument and get the value of a node in the XML document
        /// </summary>
        /// <param name="doc">XML document in string format</param>
        /// <param name="xpath">XPath of the desired node</param>
        /// <param name="value">Innertext to set for the node</param>
        /// <returns>Innertext of the node</returns>
        public string getXmlNodeValueByXpath(string doc, string xpath, string value)
        {
            //Create an XML document and load a string xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(doc);
            string nodeValue = null;

            //Find a node by xpath and get the innertext
            try
            {
                nodeValue = xmlDoc.SelectSingleNode(xpath).InnerText;
            }
            catch (XPathException xpe)
            {
                TestReporter.log("An error exisits either with the XPath [" + xpath + "] or the XML [" + doc + ".");
                TestReporter.log(xpe.StackTrace);
            }
            return nodeValue;
        }

        /// <summary>
        ///     Get the value of a node in a XML document
        /// </summary>
        /// <param name="doc">XML document in XmlDocument format</param>
        /// <param name="xpath">XPath of the desired node</param>
        /// <returns></returns>
        public string getXmlNodeValueByXpath(XmlDocument doc, string xpath)
        {
            string value = null;

            //Find a node by xpath and get the innertext
            try
            {
                value = doc.SelectSingleNode(xpath).InnerText;
            }
            catch (XPathException xpe)
            {
                TestReporter.log("An error exisits either with the XPath [" + xpath + "] or the XML [" + ConvertXmlToString(doc) + ".");
                TestReporter.log(xpe.StackTrace);
            }
            return value;
        }

        /// <summary>
        ///     Convert a string to an XmlDocument and verify the innertext of a node in a XML document
        /// </summary>
        /// <param name="doc">XML document in string format</param>
        /// <param name="xpath">XPath of the desired node</param>
        /// <param name="value">Value to verify</param>
        /// <returns>Boolean true if the value matches that which is expected, false otherwise</returns>
        public Boolean verifyXmlNodeValueByXpath(string doc, string xpath, string value)
        {
            //Create an XML document and load a string xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(doc);
            Boolean verified = false;
            //Find a node by xpath, grab the innertext and compare it to a user-defined value
            try
            {
                verified = value.Equals(xmlDoc.SelectSingleNode(xpath).InnerText);
            }
            catch (XPathException xpe)
            {
                TestReporter.log("An error exisits either with the XPath [" + xpath + "] or the XML [" + doc + ".");
                TestReporter.log(xpe.StackTrace);
            }
            return value.Equals(xmlDoc.SelectSingleNode(xpath).InnerText);
        }

        /// <summary>
        ///     Verify the innertext of a node in a XML document
        /// </summary>
        /// <param name="doc">XML document in XmlDocument format</param>
        /// <param name="xpath">XPath of the desired node</param>
        /// <param name="value">Value to verify</param>
        /// <returns>Boolean true if the value matches that which is expected, false otherwise</returns>
        public Boolean verifyXmlNodeValueByXpath(XmlDocument doc, string xpath, string value)
        {
            Boolean verified = true;
            //Find a node by xpath, grab the innertext and compare it to a user-defined value
            try
            {
                verified = value.Equals(doc.SelectSingleNode(xpath).InnerText);
            }
            catch (XPathException xpe)
            {
                TestReporter.log("An error exisits either with the XPath [" + xpath + "] or the XML [" + ConvertXmlToString(doc) + ".");
                TestReporter.log(xpe.StackTrace);
            }
            return value.Equals(doc.SelectSingleNode(xpath).InnerText);
        }

        /// <summary>
        ///     Convert an XML document into string format
        /// </summary>
        /// <param name="doc">XML document to convert to a string</param>
        /// <returns></returns>
        public string ConvertXmlToString(XmlDocument doc)
        {
            return doc.InnerText;
        }
    }
}
