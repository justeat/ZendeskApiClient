using System;
using System.Text;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Factories;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class SearchResourceFixture
    {
        private Mock<IBaseClient> _client;
        private Mock<IQueryFactory> _query;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IBaseClient>();
            _query = new Mock<IQueryFactory>();
        }
        [Test]
        public void Get_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(_client.Object);
            _query.Setup(q => q.BuildQuery()).Returns("query");

            // When
            searchResource.Get<Organization>(_query.Object);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("search")), It.Is<string>(s => s.Contains("query"))));
        }

        [Test]
        public void Get_Called_CallsGetOnClient()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            _query.Setup(q => q.BuildQuery()).Returns("query");
            var searchResource = new SearchResource(_client.Object);

            // When
            searchResource.Get<Organization>(_query.Object);

            // Then
            _client.Verify(c => c.Get<ListResponse<Organization>>(It.IsAny<Uri>()));
        }
    }
}
