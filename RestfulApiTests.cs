using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using NUnit.Framework;
using RestSharp;
using Serilog;

namespace RestfulApiValidation
{
    [TestFixture]
    public class RestfulApiTests
    {
        private readonly string baseUrl = "https://api.restful-api.dev/";
        private RestClient client;
        private string objectId;
        private readonly ILogger log = new LoggerConfiguration()
                                           .WriteTo.Console()
                                           .CreateLogger();

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
            log.Information("Client setup with base URL: {BaseUrl}", baseUrl);
        }

        [Test]
        public void Test_GetAllObjects()
        {
            var request = new RestRequest("objects", Method.Get);
            var response = client.Execute(request);

            log.Information("Status Code: {StatusCode}", response.StatusCode);
            log.Information("Response Content: {Content}", response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Content, "Expected content not to be empty");
        }

        [Test] 
        public void Test_CreateObject()
        {
            var request = new RestRequest("objects", Method.Post);
            request.AddJsonBody(new 
        { 
            name = "Apple MacBook Pro 16",
            data = new 
        {
            year = 2019,
            price = 1849.99,
            CPUmodel = "Intel Core i9",
            HardDiskSize = "1 TB"
        }
    });

        var response = client.Execute(request);
        log.Information("Status Code: {StatusCode}", response.StatusCode);
        log.Information("Response Content: {Content}", response.Content);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    
        var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(response.Content);
    

        // Store objectId for use in other tests
        objectId = responseData["id"].ToString();
}


        [Test] 
        public void Test_GetSingleObject_ShouldReturnOkStatus()
        {
            if (objectId == null) Assert.Fail("objectId not set. Run Test_CreateObject first.");
            
            var request = new RestRequest($"objects/{objectId}", Method.Get);
            var response = client.Execute(request);

            log.Information("Status Code: {StatusCode}", response.StatusCode);
            log.Information("Response Content: {Content}", response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test] //tested ok
        public void Test_DeleteObject_ShouldReturnNoContentStatus()
        {
            if (objectId == null) Assert.Fail("objectId not set. Run Test_CreateObject first.");
            
            var request = new RestRequest($"objects/{objectId}", Method.Delete);
            var response = client.Execute(request);

            log.Information("Status Code: {StatusCode}", response.StatusCode);
            log.Information("Response Content: {Content}", response.Content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
