using System;
using JustEat.ZendeskApi.Client.Http;
using JustEat.ZendeskApi.Client.Resources;
using Moq;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class AssignableGroupResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public void GetAll_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var groupResource = new AssignableGroupResource(_client.Object);

            // When
            groupResource.GetAll();

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/assignable")), ""));
        }

    }
}
