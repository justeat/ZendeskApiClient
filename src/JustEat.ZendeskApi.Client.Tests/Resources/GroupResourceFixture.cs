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
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var groupResource = new GroupsResource(_client.Object);

            // When
            groupResource.GetAll();

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("group")), ""));
        }

        [Test]
        public void Get_Called_ReturnsTicketResponse()
        {
            // Given
            var response = new GroupListResponse { Results = new List<Group> { new  Group{ Id = 1 } }};
            _client.Setup(b => b.Get<GroupListResponse>(It.IsAny<Uri>())).Returns(response);
            var groupResource = new GroupsResource(_client.Object);

            // When
            var result = groupResource.GetAll();

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
