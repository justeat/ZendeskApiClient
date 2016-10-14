using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationMembershipResourceFixture
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public void GetAllByOrganization_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            organizationMembershipResource.GetAllByOrganization(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/organizations/")), ""));
        }

        [Test]
        public async void GetAllByOrganizationAsync_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            await organizationMembershipResource.GetAllByOrganizationAsync(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/organizations/")), ""));
        }

        [Test]
        public void GetAllByOrganization_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            _client.Setup(b => b.Get<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = organizationMembershipResource.GetAllByOrganization(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAllByOrganizationAsync_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            _client.Setup(b => b.GetAsync<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(), 
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = await organizationMembershipResource.GetAllByOrganizationAsync(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void GetAllByUser_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            organizationMembershipResource.GetAllByUser(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/users/")), ""));
        }

        [Test]
        public async Task GetAllByUser_MultipleCallsAreMade_UrlIsStillCorrect()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            await organizationMembershipResource.GetAllByOrganizationAsync(1);
            organizationMembershipResource.GetAllByUser(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/users/")), ""));
        }

        [Test]
        public async void GetAllByUserAsync_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            await organizationMembershipResource.GetAllByUserAsync(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321") && st.Contains("/users/")), ""));
        }

        [Test]
        public void GetAllByUser_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            _client.Setup(b => b.Get<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = organizationMembershipResource.GetAllByUser(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAllByUserAsync_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            _client.Setup(b => b.GetAsync<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = await organizationMembershipResource.GetAllByUserAsync(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            organizationMembershipResource.GetAll(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(st => st.Contains("4321")),""));
        }

        [Test]
        public void GetAll_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            _client.Setup(b => b.Get<OrganizationMembershipListResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = organizationMembershipResource.GetAll(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Post_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { UserId = 1234 } };
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            organizationMembershipResource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Test]
        public async void PostAsync_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { UserId = 1234 } };
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            await organizationMembershipResource.PostAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.Is<string>(s => s.Contains("1234")), ""));
        }

        [Test]
        public void Post_CalledWithId_ReturnsReponseWithId()
        {
            // Given
            var response = new OrganizationMembershipResponse { Item = new OrganizationMembership { Id = 123 } };
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { Id = 123 } };
            _client.Setup(b => b.Post<OrganizationMembershipResponse>(
                It.IsAny<Uri>(),
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = organizationMembershipResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PostAsync_CalledWithId_ReturnsReponseWithId()
        {
            // Given
            var response = new OrganizationMembershipResponse { Item = new OrganizationMembership { Id = 123 } };
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { Id = 123 } };
            _client.Setup(b => b.PostAsync<OrganizationMembershipResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = await organizationMembershipResource.PostAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
