using System;
using System.Threading.Tasks;
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
        public void Get_Called_ReturnsResponse()
        {
            // Given
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Id = 1 }};
            _client.Setup(b => b.GetAsync<SatisfactionRatingResponse>(It.IsAny<Uri>())).Returns(Task.FromResult(response));
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://zendesk"));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            var result = resource.Get(321);

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
        public void Post_CalledWithRating_ReturnsUserReponse()
        {
            // Given
            var response = new SatisfactionRatingResponse { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            var request = new SatisfactionRatingRequest { Item = new SatisfactionRating { Score = SatisfactionRatingScore.good } };
            _client.Setup(b => b.PostAsync<SatisfactionRatingResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(Task.FromResult(response));
            var resource = new SatisfactionRatingResource(_client.Object);

            // When
            var result = resource.Post(request, 21);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
