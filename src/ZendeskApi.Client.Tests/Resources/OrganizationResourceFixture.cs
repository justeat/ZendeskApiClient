using System;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationResourceFixture
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
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new OrganizationResource(_client.Object);

            // When
            resource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Get_Called_ReturnsResponse()
        {
            // Given
            var response = new OrganizationResponse { Item = new Organization { Id = 1 } };
            _client.Setup(b => b.Get<OrganizationResponse>(It.IsAny<Uri>())).Returns(response);
            var resource = new OrganizationResource(_client.Object);

            // When
            var result = resource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_Called_BuildsUri()
        {
            // Given
            var request = new OrganizationRequest { Item = new Organization { Name = "Organizations", Id = 123 } };
            var resource = new OrganizationResource(_client.Object);

            // When
            resource.Put(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Put_CalledWithItem_ReturnsReponse()
        {
            // Given
            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah", Id = 123 } };
            _client.Setup(b => b.Put<OrganizationResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var resource = new OrganizationResource(_client.Object);

            // When
            var result = resource.Put(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_HasNoId_ThrowsException()
        {
            // Given
            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
            _client.Setup(b => b.Put<OrganizationResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var resource = new OrganizationResource(_client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(() => resource.Put(request));
        }

        [Test]
        public void Post_Called_BuildsUri()
        {
            // Given
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
            var resource = new OrganizationResource(_client.Object);
            
            // When
            resource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Post_CalledWithItem_ReturnsReponse()
        {
            // Given
            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
            _client.Setup(b => b.Post<OrganizationResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var resource = new OrganizationResource(_client.Object);

            // When
            var result = resource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Delete_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new OrganizationResource(_client.Object);

            // When
            resource.Delete(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Delete_Called_CallsDeleteOnClient()
        {
            // Given
            var response = new OrganizationResponse { Item = new Organization { Id = 1 } };
            _client.Setup(b => b.Get<OrganizationResponse>(It.IsAny<Uri>())).Returns(response);
            var resource = new OrganizationResource(_client.Object);

            // When
            resource.Delete(321);

            // Then
            _client.Verify(c => c.Delete(It.IsAny<Uri>()));
        }
    }
}
