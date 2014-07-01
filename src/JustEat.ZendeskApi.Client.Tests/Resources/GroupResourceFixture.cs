using System;
using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class GroupResourceFixture
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
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            groupResource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/groups/321")), ""));
        }


        [Test]
        public void Get_Called_ReturnsResponse()
        {
            // Given
            var response = new GroupResponse { Item = new Group { Id = 1 }};
            _client.Setup(b => b.Get<GroupResponse>(It.IsAny<Uri>())).Returns(response);
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            var result = groupResource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
