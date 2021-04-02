using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using RestSharp;
using System.Collections.Generic;
using Utf8Json;
using System.Linq;

namespace Sample.Test
{
    [TestClass]
    public class API_Tests
    {
        private IRestResponse response;


        [TestInitialize]
        public void Startup()
        {
            RestClient client = new RestClient("https://reqres.in/api/");
            RestRequest request = new RestRequest("users", Method.GET);
            this.response = client.Execute(request);
        }


        [TestMethod]
        public void StatusCodeTest()
        {
            Assert.AreEqual(response.StatusCode, (HttpStatusCode.OK));
        }

        [TestMethod]
        public void MoreThanZeroUsersTest()
        {

            JsonData obj = JsonSerializer.Deserialize<JsonData>(response.Content);
            List<User> users = obj.Data;
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        [DataRow("eve.holt@reqres.in")]
        [DataRow("tracey.ramos@reqres.in")]
        public void CommentAvailableForSpecificEmailTest(string email)
        {
            JsonData obj = JsonSerializer.Deserialize<JsonData>(response.Content);
            List<User> users = obj.Data;
            User expectedUser = null;

            foreach (User user in users)
            {
                if (user.Email.Equals(email))
                {
                    expectedUser = user;
                    break;
                }
            }
            Assert.IsNotNull(expectedUser);
            Assert.IsFalse(string.IsNullOrEmpty(expectedUser.Avatar));

        }

        [TestMethod]
        [DataRow(new[] { 1, 3})]
        public void FilterUsersByIdsTest(int[] expectedIds)
        {
            JsonData obj = JsonSerializer.Deserialize<JsonData>(response.Content);
            List<User> users = obj.Data;

            List<User> actualList = obj.SelectUsersWithSpecificIds(expectedIds);
            Assert.AreEqual(expectedIds.Length, actualList.Count, "Result array has incorrect number of filtered elemnets");
  
            List<int> arrWithoutExpectedIds = actualList.Select(user => user.Id).Except(expectedIds).ToList();
            Assert.AreEqual(0, arrWithoutExpectedIds.Count, "Result array does not match input data");
            
        }
    }

}
