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
    public class RequestCommentResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public void Get_Called_UrlIsCorrect()
        {
            //Given
            var response = new TicketCommentListResponse();
            _client.Setup(c => c.Get<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(response);
            var resource = new RequestCommentResource(_client.Object);

            //When
            resource.Get(321, 123);

            //Then
            _client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("requests/123/comments/321")), It.IsAny<string>()));
        }

        [Test]
        public async void GetAsync_Called_UrlIsCorrect()
        {
            //Given
            var response = new TicketCommentListResponse();
            _client.Setup(c => c.GetAsync<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new RequestCommentResource(_client.Object);

            //When
            await resource.GetAsync(321, 123);

            //Then
            _client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("requests/123/comments/321")), It.IsAny<string>()));
        }

        [Test]
        public void GetAll_CalledWithId_ReturnsListOfComments()
        {
            //Given
            var response = new TicketCommentListResponse
            {
                Results = new List<TicketComment> { new TicketComment { Id = 123 } }
            };
            _client.Setup(c => c.Get<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(response);

            var resource = new RequestCommentResource(_client.Object);

            //When
            var result = resource.GetAll(123);

            //Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAllAsync_CalledWithId_ReturnsListOfComments()
        {
            //Given
            var response = new TicketCommentListResponse
            {
                Results = new List<TicketComment> { new TicketComment { Id = 123 } }
            };
            _client.Setup(c => c.GetAsync<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new RequestCommentResource(_client.Object);

            //When
            var result = await resource.GetAllAsync(123);

            //Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void GetAll_Called_UrlIsCorrect()
        {
            //Given
            var response = new TicketCommentListResponse();
            _client.Setup(c => c.Get<TicketCommentListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var resource = new RequestCommentResource(_client.Object);

            //When
            resource.GetAll(123);

            //Then
            _client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("requests/123/comments")), It.IsAny<string>()));
        }

        [Test]
        public async void GetAllAsync_Called_UrlIsCorrect()
        {
            //Given
            var response = new TicketCommentListResponse();
            _client.Setup(c => c.GetAsync<TicketCommentListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var resource = new RequestCommentResource(_client.Object);

            //When
            await resource.GetAllAsync(123);

            //Then
            _client.Verify(c => c.BuildUri(It.Is<string>(u => u.Contains("requests/123/comments")), It.IsAny<string>()));
        }
    }
}
