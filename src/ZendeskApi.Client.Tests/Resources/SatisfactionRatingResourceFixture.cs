using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;
using ZendeskApi.Contracts.Requests;

namespace ZendeskApi.Client.Tests.Resources
{
    public class SatisfactionRatingResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public void Get_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            resource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/satisfaction_ratings/321")), ""));
        }

        [Test]
        public void Post_MultipleMethodsAreCalled_CalledUrlIsCorrect()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            resource.Get(321);
            resource.Post(new SatisfactionRatingRequest(), 1);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/tickets/1")), ""));
        }

        [Test]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            await resource.GetAsync(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/satisfaction_ratings/321")), ""));
        }
        
        [Test]
        public void Get_Called_ReturnsResponse()
        {
            // Given
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Id = 1 }};
            _client.Setup(b => b.Get<SatisfactionRatingResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            var result = resource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));

            //check the resource and operation are correctly populated
            _client.Verify(c => c.Get<SatisfactionRatingResponse>(
                It.IsAny<Uri>(),
                It.Is<string>(s => s == "SatisfactionRatingResource"),
                It.Is<string>(s => s == "Get")));
        }

        [Test]
        public async void GetAsync_Called_ReturnsResponse()
        {
            // Given
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Id = 1 }};
            _client.Setup(b => b.GetAsync<SatisfactionRatingResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            var result = await resource.GetAsync(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Post_Called_BuildsUri()
        {
            // Given
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            resource.Post(request, 231);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            await resource.PostAsync(request, 231);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Post_CalledWithRating_ReturnsUserReponse()
        {
            // Given
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            _client.Setup(b => b.Post<SatisfactionRatingResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            var result = resource.Post(request, 21);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PostAsync_CalledWithRating_ReturnsUserReponse()
        {
            // Given
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            _client.Setup(b => b.PostAsync<SatisfactionRatingResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json", 
                It.IsAny<string>(), 
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            var result = await resource.PostAsync(request, 21);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
