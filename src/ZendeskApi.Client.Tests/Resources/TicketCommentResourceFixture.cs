using System;
using System.Collections.Generic;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketCommentResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public void GetAll_CalledWithId_ReturnsListOfComments()
        {
            //Given
            var listOfTicketComments = new TicketCommentListResponse
            {
                Results = new List<TicketComment> { new TicketComment { Id = 123 } }
            };
            _client.Setup(c => c.Get<TicketCommentListResponse>(It.IsAny<Uri>())).Returns(listOfTicketComments);
            var resource = new TicketCommentResource(_client.Object);

            //When
            var result = resource.GetAll(123);

            //Then
            Assert.That(result, Is.EqualTo(listOfTicketComments));
        }

        [Test]
        public void GetAll_Called_UrlIsCorrect()
        {
            //Given
            _client.Setup(c => c.Get<TicketCommentListResponse>(It.IsAny<Uri>())).Returns(new TicketCommentListResponse());
            var resource = new TicketCommentResource(_client.Object);

            //When
            resource.GetAll(123);

            //Then
            _client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("tickets/123/comments")), It.IsAny<string>()));
        }
    }
}
