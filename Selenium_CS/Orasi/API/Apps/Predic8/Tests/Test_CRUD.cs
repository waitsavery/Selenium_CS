using System;
using System.Collections.Generic;
//NUnit
using NUnit.Framework;
//Orasi Namespaces
using Orasi.Api.Apps.Predic8.Services.ArticleService;
using Orasi.Utilities;

namespace Orasi.Api.Apps.Predic8.Tests
{
    /// <summary>
    ///     This class contains tests for the Predic8 application
    /// </summary>
    public class tests
    {
        //Define a field to hold the article ID
        string articleId = null;
        //Define a Dictionary to hold key/value pairs that will be used to create an article and verify its presence in the database
        public Dictionary<Service_ArticleService.nodes, string> nodeValues = new Dictionary<Service_ArticleService.nodes, string>();

        /// <summary>
        ///     CRUD SOAP API test
        /// </summary>
        [Test]
        public void CRUD()
        {
            //Set the test to output to the console
            TestReporter.setPrintToConsole(true);

            //Read in data to be used in the test
            readInTestData();

            //Create the article
            TestReporter.logStep("BEGIN CREATE");
            Operation_Create create = new Operation_Create();
            //Set the request by retrieving it from the warehouse
            create.setRequestFromWarehouse();
            //Define the article values in the request
            create.setRequestArticleName(nodeValues[Service_ArticleService.nodes.NAME]);
            create.setRequestArticleDescription(nodeValues[Service_ArticleService.nodes.DESCRIPTION]);
            create.setRequestArticlePriceAmount(nodeValues[Service_ArticleService.nodes.PRICEAMOUNT]);
            create.setRequestArticlePriceCurrency(nodeValues[Service_ArticleService.nodes.PRICECURRENCY]);
            create.setRequestArticleId(nodeValues[Service_ArticleService.nodes.ID]);
            //Send the request
            create.SendRequest();
            //Validate the response status code
            Assert.IsTrue(create.getResponseStatusCode().Equals("OK"));
            //Grab the article ID
            articleId = create.getResponseArticleId();
            nodeValues[Service_ArticleService.nodes.ID] = articleId;
            TestReporter.log("RESPONSE ARTICLE ID: " + articleId);
            TestReporter.logStep("END CREATE");
            TestReporter.logStep("");

            //Retrieve the article
            TestReporter.logStep("BEGIN GET");
            Operation_Get get = new Operation_Get();
            //Set the request by retrieving it from the warehouse
            get.setRequestFromWarehouse();
            //Define the article values in the request
            get.setRequestArticleId(articleId);
            //Send the request
            get.SendRequest();
            //Verify node values in the response
            Assert.IsTrue(get.getResponseStatusCode().Equals("OK"));
            Assert.IsTrue(get.getResponseArticleName().Equals(create.getRequestArticleName()));
            Assert.IsTrue(get.getResponseArticleDescription().Equals(create.getRequestArticleDescription()));
            Assert.IsTrue(get.getResponseArticlePriceAmount().Equals(create.getRequestArticlePriceAmount()));
            Assert.IsTrue(get.getResponseArticlePriceCurrency().Equals(create.getRequestArticlePriceCurrency()));
            Assert.IsTrue(RegEx.match(@"AR-\d{5}", get.getResponseArticleId(), true));
            TestReporter.logStep("END GET");
            TestReporter.logStep("");

            //Retrieve all articles
            TestReporter.logStep("BEGIN GETALL");
            Operation_GetAll getAll = new Operation_GetAll();
            //Set the request by retrieving it from the warehouse
            getAll.setRequestFromWarehouse();
            //Send the request
            getAll.SendRequest();
            //Validate the response status code
            Assert.IsTrue(getAll.getResponseStatusCode().Equals("OK"));
            //Verify that the article is in the response
            Assert.IsTrue(getAll.verifyArticleInResponse(nodeValues), "\n**********\nThe article was not found\n**********");
            TestReporter.logStep("END GETALL");
            TestReporter.logStep("");

            //Delete the article
            TestReporter.logStep("BEGIN DELETE");
            Operation_Delete delete = new Operation_Delete();
            //Set the request by retrieving it from the warehouse
            delete.setRequestFromWarehouse();
            //Define the article values in the request
            delete.setRequestArticleId(articleId);
            //Send the request
            delete.SendRequest();
            //Validate the response status code
            Assert.IsTrue(delete.getResponseStatusCode().Equals("OK"));
            TestReporter.logStep("END DELETE");
            TestReporter.logStep("");
        }

        /// <summary>
        ///     Generate values to create and validate the article in the database
        /// </summary>
        public void readInTestData()
        {
            nodeValues.Add(Service_ArticleService.nodes.DESCRIPTION, Randomness.randomAlphaNumeric(8));
            nodeValues.Add(Service_ArticleService.nodes.ID, "AR-12345");
            nodeValues.Add(Service_ArticleService.nodes.NAME, Randomness.randomString(8));
            nodeValues.Add(Service_ArticleService.nodes.PRICEAMOUNT, "1.0");
            nodeValues.Add(Service_ArticleService.nodes.PRICECURRENCY, "USD");
        }
    }
}
