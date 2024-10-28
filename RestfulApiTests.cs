using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using NUnit.Framework;
using RestSharp;

namespace RestfulApiValidation
{
    [TestFixture]
    public class RestfulApiTests
    {
        private readonly string baseUrl = "https://api.restful-api.dev/";
        private RestClient client;
        private string objectId;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }

        [Test,Order(1)]
        public void Test_GetAllObjects()
        {
            var request = new RestRequest("objects", Method.Get);
            var response = client.Execute(request);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {response.Content}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Content, "Expected content not to be empty");
        }


        [Test,Order(3)]
        public void Test_GetSingleObject()
        {
            var request = new RestRequest($"objects/{objectId}", Method.Get);
            var response = client.Execute(request);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {response.Content}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Content, "Expected content not to be empty");
        }

        [Test,Order(2)]
        public void Test_AddObject()
        {
            var request = new RestRequest("objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                name = "Sample Object",
                data = new
                {
                    year = 2023,
                    price = 999.99,
                    CPU_model = "Intel Core i7",
                    Hard_disk_size = "512 GB"
                }
            });

            var response = client.Execute(request);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {response.Content}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(response.Content);
            if (jsonResponse != null && jsonResponse.ContainsKey("id"))
            {
                objectId = jsonResponse["id"].ToString();
            }
            else
            {
                Assert.Fail("Failed to retrieve 'id' from the response content.");
            }
        }


        [Test,Order(4)]
        public void Test_UpdateObject()
        {
            var request = new RestRequest($"objects/{objectId}", Method.Put);
            request.AddJsonBody(new
            {
                name = "Apple MacBook Pro 16",
                data = new
                {
                    year = 2019,
                    price = 2049.99,
                    CPUmodel = "Intel Core i9",
                    HardDiskSize = "1 TB",
                    color = "silver"
                }
            });

            var response = client.Execute(request);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {response.Content}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Test,Order(5)]
        public void Test_DeleteObject()
        {
            var request = new RestRequest($"objects/{objectId}", Method.Delete);
            var response = client.Execute(request);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {response.Content}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
