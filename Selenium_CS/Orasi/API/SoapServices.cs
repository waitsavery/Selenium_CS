using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
//Orasi Namespaces
using Orasi.Utilities;

namespace Orasi.Api
{
    /// <summary>
    ///     This class contains fields and methods intended to build, parse, modify and send a SOAP Web Service request, 
    ///     as well as retrieve and parse the response.  
    ///     Interaction with the SOAP request and response XMLs is facilitated by extending the XMLTools class
    /// </summary>
    public class SoapServices : XMLTools
    {
        //Define a field to house the request XMl path
        private string warehouseRequestPath;
        //Define fields to hold the SOAP request as different types
        private string strRequestXml;
        private XmlDocument xmlRequestXml;
        //Define fields to hold the SOAP response as different types
        private string strResponseXml;
        private XmlDocument xmlResponseXml;
        //Define a field to store the WSDL URI
        private string strWsdlUri;
        //Define a field to hold the SOAP envelope
        private XmlDocument soapEnvelopeXml;
        //Define a field to hold the HTTP request
        private HttpWebRequest httpRequest;
        //Define a field to hold the HTTP response
        private HttpWebResponse httpResponse;
        //Define a field to hold the SOAP response status code
        private string statusCode;

        /// <summary>
        ///     Execute a Soap WebService call
        /// </summary>
        public void SendRequest()
        {
            //Create a HTTP request object and send the request
            CreateWebRequest();
            //Retrieve the response
            GetResponse();
        }

        /// <summary>
        ///     Create a SOAP webrequest to [Url]
        /// </summary>
        /// <returns>HttpWebRequest for a specicifc service</returns>
        public void CreateWebRequest()
        {
            //Create a HTTP request using a service WSDL URI
            httpRequest = (HttpWebRequest)WebRequest.Create(getWsdlUriString());
            //Add headers into the collection
            httpRequest.Headers.Add(@"SOAP:Action");
            //Add HTTP content type header
            httpRequest.ContentType = "text/xml;charset=\"utf-8\"";
            //Add HTTP accept type
            httpRequest.Accept = "text/xml";
            //Add HTTP request method
            httpRequest.Method = "POST";

            //Generate the SOAP envelope by loading the request from the request warehouse
            soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(getRequestXmlAsString());

            //****************************
            //****************************
            //*** BEGIN MODIFY REQUEST ***
            //****************************
            //****************************


            //**************************
            //**************************
            //*** END MODIFY REQUEST ***
            //**************************
            //**************************

            //Report the final version of the request
            TestReporter.logSoapXml("SOAP REQUEST XML: " + strRequestXml);
            //Create a stream object to which to write request data and save the request XML to the new stream
            try
            {
                //Create the stream object
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    //Save the XMl to the stream
                    soapEnvelopeXml.Save(stream);
                }
            }
            catch (WebException we)
            {
                TestReporter.log("An error occurred during the SOAP request: " + we.StackTrace);
            }
        }

        /// <summary>
        ///     Get the SOAP response and save as an XmlDocument and string
        /// </summary>
        private void GetResponse()
        {
            try
            {
                using (WebResponse response = httpRequest.GetResponse())
                {
                    //Get the response from the service
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        //Set the response as a string
                        setResponseXmlAsString(rd.ReadToEnd());
                        //Set the response as an XmlDocument
                        setResponseXml(getResponseXmlAsString());
                        TestReporter.logSoapXml("SOAP RESPONSE XML: " + strResponseXml);
                    }
                    //Create an HTTP response object
                    httpResponse = (HttpWebResponse)response;

                    //Set the response status code. A value of "OK" represents a successful request.
                    setResponseStatusCode(httpResponse.StatusDescription);
                }
            }
            catch (WebException we)
            {
                TestReporter.log("An error occurred during the SOAP response: " + we.StackTrace);
            }
        }

        /// <summary>
        ///     Set the WSDL URI as a string
        /// </summary>
        /// <param name="endpoint">WSDL URI</param>
        public void setWsdlUriString(string endpoint)
        {
            string ending;
            //Append the service URI with a substring based on the beginning of the URI
            if (endpoint.ToLower().Contains("http"))
            {
                ending = "?wsdl";
            }
            else
            {
                ending = ".wsdl";
            }
            strWsdlUri = endpoint + ending;
            TestReporter.log("WSDL URI: " + strWsdlUri);
        }

        /// <summary>
        ///     Get the WSDL URI
        /// </summary>
        /// <returns>WSDL URI as a string</returns>
        public string getWsdlUriString()
        {
            return strWsdlUri;
        }

        /// <summary>
        ///     Grab a specific request from the request warehouse and populate the request string and XmlDocument objects
        /// </summary>
        public void setRequestFromWarehouse()
        {
            //Set the initial request XML as a string
            setRequestXmlAsString(File.ReadAllText(warehouseRequestPath));
            //Set the initial request XML as an XmlDocument
            setRequestXml(getRequestXmlAsString());
        }

        /// <summary>
        ///     Set the SOAP request xml as a string
        /// </summary>
        /// <param name="xml">SOAP request xml</param>
        public void setRequestXmlAsString(string xml)
        {
            //Trim any leading and trailing whitespaces from each line in the XML
            strRequestXml = Regex.Replace(xml, @">\s*<", "><");
            //Remove any carriage return or newline characters in the request XML
            strRequestXml = xml.Replace("\r\n", "");
        }

        /// <summary>
        ///     Get the SOAP request
        /// </summary>
        /// <returns>SOAP reuest as a string</returns>
        public string getRequestXmlAsString()
        {
            return strRequestXml;
        }

        /// <summary>
        ///     Set the SOAP request as a XmlDocument
        /// </summary>
        /// <param name="xmlString">XML as a string</param>
        public void setRequestXml(string xmlString)
        {
            xmlRequestXml = new XmlDocument();
            xmlRequestXml.LoadXml(xmlString);
        }

        /// <summary>
        ///     Get the SOAP request
        /// </summary>
        /// <returns>SOAP request as a XmlDocument</returns>
        public XmlDocument getRequestXml()
        {
            return xmlRequestXml;
        }

        /// <summary>
        ///     Set the SOAP response as a string
        /// </summary>
        /// <param name="response">SOAP response xml</param>
        public void setResponseXmlAsString(string response)
        {
            strResponseXml = response;
        }

        /// <summary>
        ///     Get the SOAP response
        /// </summary>
        /// <returns>SOAP response as a string</returns>
        public string getResponseXmlAsString()
        {
            return strResponseXml;
        }

        /// <summary>
        ///     Set the SOAP response as a XmlDocument
        /// </summary>
        /// <param name="xmlString"></param>
        public void setResponseXml(string xmlString)
        {
            xmlResponseXml = new XmlDocument();
            xmlResponseXml.LoadXml(xmlString);
        }

        /// <summary>
        ///     Get the SOAP response
        /// </summary>
        /// <returns>SOAP response as a XmlDocument</returns>
        public XmlDocument getResponseXml()
        {
            return xmlResponseXml;
        }

        /// <summary>
        ///     Set the SOAP response code.
        ///     "OK" indicates a successful request.
        /// </summary>
        /// <param name="responseCode">SOAP response code</param>
        private void setResponseStatusCode(string responseCode)
        {
            statusCode = responseCode;
        }

        /// <summary>
        ///     Get the SOAP response code
        /// </summary>
        /// <returns>SOAP response code</returns>
        public string getResponseStatusCode()
        {
            return statusCode;
        }

        /// <summary>
        ///     Set a request XML node value by xpath
        /// </summary>
        /// <param name="xpath">XPath of the desired node</param>
        /// <param name="value">Innertext to set for the node</param>
        public void setRequestNodeValueByXpath(string xpath, string value)
        {
            //Redefine the requset XML by modifying a node's value in the request
            xmlRequestXml = setXmlNodeValueByXpath(xmlRequestXml, xpath, value);
            //Sets the string version of the request XML with the newly modified XML request
            strRequestXml = xmlRequestXml.InnerXml;
        }

        /// <summary>
        ///     Get a request XML node value by xpath
        /// </summary>
        /// <param name="xpath">XPath of the desired node</param>
        /// <returns>Inner text of the desired node</returns>
        public string getRequestNodeValueByXpath(string xpath)
        {
            return getXmlNodeValueByXpath(xmlRequestXml, xpath);
        }

        /// <summary>
        ///     Get a response XML node value by xpath
        /// </summary>
        /// <param name="xpath">XPath of the desired node</param>
        /// <returns>Inner text of the desired node</returns>
        public string getResponseNodeValueByXpath(string xpath)
        {
            return getXmlNodeValueByXpath(xmlResponseXml, xpath);
        }

        /// <summary>
        ///     Set the path for the request XML
        /// </summary>
        /// <param name="path">Request XML warehouse path</param>
        public void setRequestWarehouseRequestPath(string path)
        {
            warehouseRequestPath = path;
        }

        /// <summary>
        ///     Get the path for the request XML
        /// </summary>
        /// <returns></returns>
        public string getRequestWarehouseRequestPath()
        {
            return warehouseRequestPath;
        }
    }
}
