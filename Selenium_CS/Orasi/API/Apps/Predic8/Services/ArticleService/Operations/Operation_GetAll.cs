using System;
using System.Collections.Generic;
using System.Xml;

namespace Orasi.Api.Apps.Predic8.Services.ArticleService
{
    /// <summary>
    ///     This class contains fields and methods used to interact with the 
    ///     GetAll operation in the ArticleService of the Predic8 application.
    ///     The ArticleService class is extended, therefore basic SOAP and XML 
    ///     fields and methods are inherited.
    /// </summary>
    public class Operation_GetAll : Service_ArticleService
    {
        //SOAP Request XML warehouse location
        private string requestPath = @"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CSharp\mySelenium\Orasi.Api.Apps.Predic8.Services.ArticleService.Operations.RequestXmlWarehouse\GetAll.xml";

        //Commonly used request xpaths

        //Commonly used response xpaths
        private string rsArticleNodesXpath = "/*[local-name(.)='Envelope'][1]/*[local-name(.)='Body'][1]/*[local-name(.)='getAllResponse'][1]/*[local-name(.)='article']";
        private string rsArticleIdNodeXpath = "id";
        private string rsArticleNameNodeXpath = "name";
        private string rsArticleDescriptionNodeXpath = "description";
        private string reArticlePriceNodeXpath = "price";
        private string rsArticlePriceAmountNodeXpath = "price/amount";
        private string rsArticlePriceCurrencyNodeXpath = "price/currency";

        /// <summary>
        ///     Constructor that sets the request XML
        /// </summary>
        public Operation_GetAll()
        {
            setRequestWarehouseRequestPath(requestPath);
        }

        /// <summary>
        ///     Verify that an article is contained in the response
        /// </summary>
        /// <param name="nodeValues">Dictionary of key/value pairs used to determine if an article is 
        /// contained in the resposne. All pairs must be contained in order to deem the article 
        /// contained in the response</param>
        /// <returns>Boolean true if the article is contained in the response, false otherwise</returns>
        public Boolean verifyArticleInResponse(Dictionary<nodes, string> nodeValues)
        {
            //Define a field used to determine if the article was found in the reponse
            Boolean articleFound = false;
            //Define a field used to determine if the article name was found in a response node
            Boolean nameVerified = false;
            //Define a field used to determine if the article description was found in a response node
            Boolean descriptionVerified = false;
            //Define a field used to determine if the article price was found in a response node
            Boolean priceVerified = false;
            //Define a field used to determine if the article currency was found in a response node
            Boolean currencyVerified = false;
            //Define a field used to determine if the article ID was found in a response node
            Boolean idVerified = false;

            //Grab all article nodes in the response
            XmlNodeList nodesList = getResponseXml().SelectNodes(rsArticleNodesXpath);

            //Search each article node for node values for a specific article
            foreach (XmlNode node in nodesList)
            {
                //Verify Article Name
                nameVerified = node.SelectSingleNode(rsArticleNameNodeXpath).InnerXml.Equals(nodeValues[nodes.NAME]);
                //Verify Article Description
                descriptionVerified = node.SelectSingleNode(rsArticleDescriptionNodeXpath).InnerXml.Equals(nodeValues[nodes.DESCRIPTION]);
                //Verify Article Price. First, grab the price node.
                XmlNode priceNode = node.SelectSingleNode(reArticlePriceNodeXpath);
                //Ensure the price node object is not null
                if (priceNode != null)
                {
                    //Ensure the price node has child nodes
                    if (priceNode.ChildNodes.Count != 0)
                    {
                        //Verify Article Price Amount
                        priceVerified = node.SelectSingleNode(rsArticlePriceAmountNodeXpath).InnerXml.Equals(nodeValues[nodes.PRICEAMOUNT]);
                        //Verify Article Price Currency
                        currencyVerified = node.SelectSingleNode(rsArticlePriceCurrencyNodeXpath).InnerXml.Equals(nodeValues[nodes.PRICECURRENCY]);
                    }
                }
                //Verify Article Id
                idVerified = node.SelectSingleNode(rsArticleIdNodeXpath).InnerXml.Equals(nodeValues[nodes.ID]);
                //If all node values are verified, then the article is deemed to be in the response
                if (nameVerified && descriptionVerified && priceVerified && currencyVerified && idVerified)
                {
                    articleFound = true;
                    break;
                }
                //If the article is not found in the response, reset all validators to false
                nameVerified = false;
                descriptionVerified = false;
                priceVerified = false;
                currencyVerified = false;
                idVerified = false;
            }

            return articleFound;
        }

        //*********************************************
        //*** Getters and Setters for request nodes ***
        //*********************************************

        //**********************************
        //*** Getters for response nodes ***
        //**********************************
    }
}
