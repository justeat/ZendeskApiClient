using System;
using System.Collections.Generic;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Resources
{
    public class UserIdentityResourceFixture
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
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var userIdentityResource = new UserIdentityResource(_client.Object);

            // When
            userIdentityResource.GetAll(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321")),""));
        }

        [Test]
        public void GetAll_Called_ReturnsUserIdentityResponse()
        {
            // Given
            var response = new UserIdentityListResponse { Results = new List<UserIdentity> { new UserIdentity { Id = 1 } } };
            _client.Setup(b => b.Get<UserIdentityListResponse>(It.IsAny<Uri>())).Returns(response);
            var userIdentityResource = new UserIdentityResource(_client.Object);

            // When
            var result = userIdentityResource.GetAll(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Post_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email", UserId = 1234} };
            var userIdentityResource = new UserIdentityResource(_client.Object);

            // When
            userIdentityResource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Test]
        public void Post_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserIdentityResponse { Item = new UserIdentity { Name = "email" } };
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email" } };
            _client.Setup(b => b.Post<UserIdentityResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var userIdentityResource = new UserIdentityResource(_client.Object);

            // When
            var result = userIdentityResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email", UserId = 1234, Id = 123} };
            var userIdentityResource = new UserIdentityResource(_client.Object);

            // When
            userIdentityResource.Put(request);

            // Then
            _client.Setup(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Test]
        public void Put_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserIdentityResponse { Item = new UserIdentity { Name = "email", Id = 123} };
            var request = new UserIdentityRequest { Item = new UserIdentity { Name = "email", Id = 123} };
            _client.Setup(b => b.Put<UserIdentityResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var userIdentityResource = new UserIdentityResource(_client.Object);

            // When
            var result = userIdentityResource.Put(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
