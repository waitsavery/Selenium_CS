using System;

namespace Orasi.Api.Apps.Predic8.Services.ArticleService
{
    /// <summary>
    ///     This class defines the service URL for the ArticleService of the Predic8 application, 
    ///     and enumerated values for some values for response nodes.
    ///     Basic SOAP methods are inherited from the SoapServices class.
    /// </summary>
    public class Service_ArticleService : Orasi.Api.SoapServices
    {
        //Define the service URI
        private string serviceUrl = "http://www.predic8.com:8080/material/ArticleService";

        //Define enumerations used to interact with request/response nodes
        public enum nodes { NAME, DESCRIPTION, PRICEAMOUNT, PRICECURRENCY, ID }

        /// <summary>
        ///     Constructor that sets the service URL
        /// </summary>
        public Service_ArticleService()
        {
            setWsdlUriString(serviceUrl);
        }
    }
}
