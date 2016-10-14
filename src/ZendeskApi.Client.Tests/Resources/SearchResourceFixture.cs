using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Queries;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Resources
{
    public class SearchResourceFixture
    {
        private Mock<IRestClient> _client;
        private Mock<IZendeskQuery<Organization>> _query;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
            _query = new Mock<IZendeskQuery<Organization>>();
        }

        [Test]
        public void Find_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(_client.Object);
            _query.Setup(q => q.BuildQuery()).Returns("query");

            // When
            searchResource.Find(_query.Object);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("search")), It.Is<string>(s => s.Contains("query"))));
        }

        [Test]
        public async void FindAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(_client.Object);
            _query.Setup(q => q.BuildQuery()).Returns("query");

            // When
            await searchResource.FindAsync(_query.Object);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("search")), It.Is<string>(s => s.Contains("query"))));
        }

        [Test]
        public void Find_Called_CallsGetOnClient()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            _query.Setup(q => q.BuildQuery()).Returns("query");
            var searchResource = new SearchResource(_client.Object);

            // When
            searchResource.Find(_query.Object);

            // Then
            _client.Verify(c => c.Get<ListResponse<Organization>>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()));
        }

        [Test]
        public async void FindAsync_Called_CallsGetOnClient()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            _query.Setup(q => q.BuildQuery()).Returns("query");
            var searchResource = new SearchResource(_client.Object);

            // When
            await searchResource.FindAsync(_query.Object);

            // Then
            _client.Verify(c => c.GetAsync<ListResponse<Organization>>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()));
        }
    }
}
