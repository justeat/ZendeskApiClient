using System;
using System.Text;
using JE.Api.ClientBase;
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

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IBaseClient>();
        }

        [Test]
        public void Get_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(_client.Object);

            // When
            searchResource.Get<Organization>(ZendeskType.Organization, "somecustomid", 321);

            // Then
            _client.Verify(c => c.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))));
        }

        [Test]
        public void Get_Called_CallsGetOnClient()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321"))))
                .Returns(new Uri("http://search"));
            var searchResource = new SearchResource(_client.Object);

            // When
            searchResource.Get<Organization>(ZendeskType.Organization, "somecustomid", 321);

            // Then
            _client.Verify(c => c.Get<ListResponse<Organization>>(It.IsAny<Uri>()));
        }
    }
}
