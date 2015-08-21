using System;

namespace Orasi.Api.Apps.Predic8.Services.ArticleService
{
    /// <summary>
    ///     This class contains fields and methods used to interact with the 
    ///     Create operation in the ArticleService of the Predic8 application.
    ///     The ArticleService class is extended, therefore basic SOAP and XML 
    ///     fields and methods are inherited.
    /// </summary>
    public class Operation_Create : Service_ArticleService
    {
        //SOAP Request XML warehouse location
        private string requestPath = @"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CS\Selenium_CS\Orasi\API\Apps\Predic8\Services\ArticleService\Operations\RequestXmlWarehouse\Create.xml";

        //Commonly used request xpaths
        private string rqArticleNameXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='create'][1]/*[local-name(.)='article'][1]/*[local-name(.)='name'][1]";
        private string rqArticleDescriptionXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='create'][1]/*[local-name(.)='article'][1]/*[local-name(.)='description'][1]";
        private string rqArticlePriceAmountXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='create'][1]/*[local-name(.)='article'][1]/*[local-name(.)='price'][1]/*[local-name(.)='amount'][1]";
        private string rqArticlePriceCurrencyXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='create'][1]/*[local-name(.)='article'][1]/*[local-name(.)='price'][1]/*[local-name(.)='currency'][1]";
        private string rqArticleIdXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='create'][1]/*[local-name(.)='article'][1]/*[local-name(.)='id'][1]";

        //Commonly used response xpaths
        private string rsArticleIdXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='createResponse'][1]/*[local-name(.)='id'][1]";

        /// <summary>
        ///     Constructor that sets the request XML
        /// </summary>
        public Operation_Create()
        {
            setRequestWarehouseRequestPath(requestPath);
        }

        //*********************************************
        //*** Getters and Setters for request nodes ***
        //*********************************************
        /// <summary>
        ///     Set the article name in the request
        /// </summary>
        /// <param name="name">Article name</param>
        public void setRequestArticleName(string name)
        {
            setRequestNodeValueByXpath(rqArticleNameXpath, name);
        }

        /// <summary>
        ///     Get the article name from the request
        /// </summary>
        /// <returns>string article name</returns>
        public string getRequestArticleName()
        {
            return getRequestNodeValueByXpath(rqArticleNameXpath);
        }

        /// <summary>
        ///     Set the article description in the request
        /// </summary>
        /// <param name="desc">Artice description</param>
        public void setRequestArticleDescription(string desc)
        {
            setRequestNodeValueByXpath(rqArticleDescriptionXpath, desc);
        }

        /// <summary>
        ///     Get the article description from the request
        /// </summary>
        /// <returns>string article description</returns>
        public string getRequestArticleDescription()
        {
            return getRequestNodeValueByXpath(rqArticleDescriptionXpath);
        }

        /// <summary>
        ///     Set the article price amount in the request
        /// </summary>
        /// <param name="amt">Article price amount</param>
        public void setRequestArticlePriceAmount(string amt)
        {
            setRequestNodeValueByXpath(rqArticlePriceAmountXpath, amt);
        }

        /// <summary>
        ///     Get the article price amount from the request
        /// </summary>
        /// <returns>string article price amount</returns>
        public string getRequestArticlePriceAmount()
        {
            return getRequestNodeValueByXpath(rqArticlePriceAmountXpath);
        }

        /// <summary>
        ///     Set the article price currency in the request
        /// </summary>
        /// <param name="cur">Article price currency</param>
        public void setRequestArticlePriceCurrency(string cur)
        {
            setRequestNodeValueByXpath(rqArticlePriceCurrencyXpath, cur);
        }

        /// <summary>
        ///     Get the article price currency from the request
        /// </summary>
        /// <returns>string article price currency</returns>
        public string getRequestArticlePriceCurrency()
        {
            return getRequestNodeValueByXpath(rqArticlePriceCurrencyXpath);
        }

        /// <summary>
        ///     Set the article ID in the request
        /// </summary>
        /// <param name="id">Article ID</param>
        public void setRequestArticleId(string id)
        {
            setRequestNodeValueByXpath(rqArticleIdXpath, id);
        }

        /// <summary>
        ///     Get the article ID from the request
        /// </summary>
        /// <returns>string article ID</returns>
        public string getRequestArticleId()
        {
            return getRequestNodeValueByXpath(rqArticleIdXpath);
        }

        //**********************************
        //*** Getters for response nodes ***
        //**********************************
        /// <summary>
        ///     Get the article ID from the response
        /// </summary>
        /// <returns>string article ID</returns>
        public string getResponseArticleId()
        {
            return getResponseNodeValueByXpath(rsArticleIdXpath);
        }
    }
}
