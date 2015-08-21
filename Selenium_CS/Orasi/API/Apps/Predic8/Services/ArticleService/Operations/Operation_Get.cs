using System;

namespace Orasi.Api.Apps.Predic8.Services.ArticleService
{
    /// <summary>
    ///     This class contains fields and methods used to interact with the 
    ///     Get operation in the ArticleService of the Predic8 application.
    ///     The ArticleService class is extended, therefore basic SOAP and XML 
    ///     fields and methods are inherited.
    /// </summary>
    public class Operation_Get : Service_ArticleService
    {
        //SOAP Request XML warehouse location
        private string requestPath = @"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CS\Selenium_CS\Orasi\API\Apps\Predic8\Services\ArticleService\Operations\RequestXmlWarehouse\Get.xml";

        //Commonly used request xpaths
        private string rqArticleIdXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='get'][1]/*[local-name(.)='id'][1]";

        //Commonly used response xpaths
        private string rsArticleNameXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='getResponse'][1]/*[local-name(.)='article'][1]/*[local-name(.)='name'][1]";
        private string rsArticleDescriptionXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='getResponse'][1]/*[local-name(.)='article'][1]/*[local-name(.)='description'][1]";
        private string rsArticlePriceAmountXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='getResponse'][1]/*[local-name(.)='article'][1]/*[local-name(.)='price'][1]/*[local-name(.)='amount'][1]";
        private string rsArticlePriceCurrencyXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='getResponse'][1]/*[local-name(.)='article'][1]/*[local-name(.)='price'][1]/*[local-name(.)='currency'][1]";
        private string rsArticleIdXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='getResponse'][1]/*[local-name(.)='article'][1]/*[local-name(.)='id'][1]";

        /// <summary>
        ///     Constructor that sets the request XML
        /// </summary>
        public Operation_Get()
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
            setRequestNodeValueByXpath(rqArticleIdXpath, id);
        }

        //*********************************************
        //*** Getters and Setter for response nodes ***
        //*********************************************
        /// <summary>
        ///     Get the article name from the response
        /// </summary>
        /// <returns>string article name</returns>
        public string getResponseArticleName()
        {
            return getResponseNodeValueByXpath(rsArticleNameXpath);
        }

        /// <summary>
        ///     Get the article description from the response
        /// </summary>
        /// <returns>string article description</returns>
        public string getResponseArticleDescription()
        {
            return getResponseNodeValueByXpath(rsArticleDescriptionXpath);
        }

        /// <summary>
        ///     Get the response article price amount from the response
        /// </summary>
        /// <returns>string article price amount</returns>
        public string getResponseArticlePriceAmount()
        {
            return getResponseNodeValueByXpath(rsArticlePriceAmountXpath);
        }

        /// <summary>
        ///     Get the response article price currency from the response
        /// </summary>
        /// <returns>string article price currency</returns>
        public string getResponseArticlePriceCurrency()
        {
            return getResponseNodeValueByXpath(rsArticlePriceCurrencyXpath);
        }

        /// <summary>
        ///     Get the response article ID
        /// </summary>
        /// <returns>string article ID</returns>
        public string getResponseArticleId()
        {
            return getResponseNodeValueByXpath(rsArticleIdXpath);
        }
    }
}
