using System;
using System.Collections.Generic;
using System.Text;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Serialization;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class SearchResourceFixture
    {
        private Mock<IBaseClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IBaseClient>();
        }

        [Test]
        public void call()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", "zendeskapi@just-eat.com/token", "R8CvArGs3sOWoK0muPh8r39XocxylQEUwV2jFL0a")));
            var header = string.Format("Basic {0}", auth);

            var resource = new SearchResource(new BaseClient(new Uri("https://justeatukpoc1399564481.zendesk.com"),
                new ZendeskDefaultConfiguration { Authorization = header }));

            var result = resource.Get<TicketsResponse>("ticket", "restaurantid", 7385);
        }

        [Test]
        public void Get_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(_client.Object);

            // When
            searchResource.Get<TicketsResponse>("ticket", "somecustomid", 321);

            // Then
            _client.Verify(c => c.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))));
        }
    }
}
