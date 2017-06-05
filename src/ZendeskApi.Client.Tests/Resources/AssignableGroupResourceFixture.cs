using System;
using Xunit;

namespace ZendeskApi.Client.Tests.Resources
{
    public class AssignableGroupResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Fact]
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


        [Fact]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var groupResource = new AssignableGroupResource(_client.Object);

            // When
            await groupResource.GetAllAsync();

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/assignable")), ""));
        }

    }
}
