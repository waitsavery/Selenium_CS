using System;

namespace Orasi.Api.Apps.Predic8.Services.ArticleService
{
    /// <summary>
    ///     This class contains fields and methods used to interact with the 
    ///     Delete operation in the ArticleService of the Predic8 application.
    ///     The ArticleService class is extended, therefore basic SOAP and XML 
    ///     fields and methods are inherited.
    /// </summary>
    public class Operation_Delete : Service_ArticleService
    {
        //SOAP Request XML warehouse location
        private string requestPath = @"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CSharp\mySelenium\Orasi.Api.Apps.Predic8.Services.ArticleService.Operations.RequestXmlWarehouse\Delete.xml";

        //Commonly used request xpaths
        private string requestArticleIdXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='delete'][1]/*[local-name(.)='id'][1]";

        /// <summary>
        ///     Constructor that sets the request XML
        /// </summary>
        public Operation_Delete()
        {
            setRequestWarehouseRequestPath(requestPath);
        }

        //*********************************************
        //*** Getters and Setters for request nodes ***
        //*********************************************
        /// <summary>
        ///     Set the article ID in the request
        /// </summary>
        /// <param name="id">Article ID</param>
        public void setRequestArticleId(string id)
        {
            setRequestNodeValueByXpath(requestArticleIdXpath, id);
        }

        //*********************************************
        //*** Getters and Setter for response nodes ***
        //*********************************************
    }
}
