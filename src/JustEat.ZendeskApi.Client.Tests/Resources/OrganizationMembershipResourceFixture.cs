using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class OrganizationMembershipResourceFixture
    {
        private Mock<IBaseClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IBaseClient>();
        }

        [Test]
        public void GetAll_Called_CallsBuildUriWithFieldId()
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
            _client.Setup(b => b.Get<OrganizationMembershipListResponse>(It.IsAny<Uri>())).Returns(response);
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
        public void Post_CalledWithId_ReturnsReponseWithId()
        {
            // Given
            var response = new OrganizationMembershipResponse { Item = new OrganizationMembership { Id = 123 } };
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { Id = 123 } };
            _client.Setup(b => b.Post<OrganizationMembershipResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var organizationMembershipResource = new OrganizationMembershipResource(_client.Object);

            // When
            var result = organizationMembershipResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
