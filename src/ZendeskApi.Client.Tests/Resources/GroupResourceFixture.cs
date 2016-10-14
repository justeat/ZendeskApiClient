using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Resources
{
    public class GroupResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            await groupResource.GetAsync(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/groups/321")), ""));
        }

        [Test]
        public void Get_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            groupResource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/groups/321")), ""));
        }


        [Test]
        public async void GetAsync_Called_ReturnsResponse()
        {
            // Given
            var response = new GroupResponse { Item = new Group { Id = 1 }};
            _client.Setup(b => b.GetAsync<GroupResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                ))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            var result = await groupResource.GetAsync(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Get_Called_ReturnsResponse()
        {
            // Given
            var response = new GroupResponse { Item = new Group { Id = 1 }};
            _client.Setup(b => b.Get<GroupResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            var result = groupResource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
