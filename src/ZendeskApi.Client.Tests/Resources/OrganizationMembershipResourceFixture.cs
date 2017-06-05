using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationMembershipResourceFixture
    {
        private Mock<IZendeskApiClient> _apiClient;
        private Mock<HttpClient> _httpClient;

        public OrganizationMembershipResourceFixture()
        {
            _apiClient = new Mock<IZendeskApiClient>();
            _httpClient = new Mock<HttpClient>();
        }

        [Fact]
        public async Task GetAllByOrganizationAsync_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);
            var organizationMembershipResource = new OrganizationMembershipResource(_apiClient.Object);

            // When
            await organizationMembershipResource.GetAllByOrganizationAsync(4321);

            // Then
            _apiClient.Verify(c => c.CreateClient(It.Is<string>(x => x.Contains("organizations"))), "");
            _httpClient.Verify(c => c.GetAsync("4321"), "");
        }

        [Fact]
        public async Task GetAllByOrganization_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse { Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
            _httpClient.Setup(b => b.GetAsync(It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(message));
            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);

            var organizationMembershipResource = new OrganizationMembershipResource(_apiClient.Object);

            // When
            var result = await organizationMembershipResource.GetAllByOrganizationAsync(4321);

            // Then
            Assert.Equal(response, result);
        }

        [Fact]
        public async void GetAllByUserAsync_Called_CallsBuildUriWithFieldIdAndType()
        {
            // Given
            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);
            var organizationMembershipResource = new OrganizationMembershipResource(_apiClient.Object);

            // When
            await organizationMembershipResource.GetAllByUserAsync(4321);

            // Then
            _apiClient.Verify(c => c.CreateClient(It.Is<string>(x => x.Contains("users"))), "");
            _httpClient.Verify(c => c.GetAsync("4321"), "");
        }

        [Fact]
        public async void GetAllByUserAsync_Called_ReturnsOrganizationMembershipResponse()
        {
            // Given
            var response = new OrganizationMembershipListResponse {
                Results = new List<OrganizationMembership> { new OrganizationMembership { Id = 1 } } };
            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
            _httpClient.Setup(b => b.GetAsync(It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(message));
            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);

            var organizationMembershipResource = new OrganizationMembershipResource(_apiClient.Object);

            // When
            var result = await organizationMembershipResource.GetAllByUserAsync(4321);

            // Then
            Assert.Equal(response, result);
        }

        [Fact]
        public async void PostAsync_Called_BuildsUriWithFieldUserId()
        {
            // Given
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { UserId = 1234 } };
            var organizationMembershipResource = new OrganizationMembershipResource(_apiClient.Object);

            // When
            await organizationMembershipResource.PostAsync(request);

            // Then
            _httpClient.Verify(c => c.GetAsync("4321"), "");
        }

        [Fact]
        public async void PostAsync_CalledWithId_ReturnsReponseWithId()
        {
            // Given
            var response = new OrganizationMembershipResponse { Item = new OrganizationMembership { Id = 123 } };
            var request = new OrganizationMembershipRequest { Item = new OrganizationMembership { Id = 123 } };

            _httpClient.Setup(b => b.PostAsync(It.IsAny<string>(), new StringContent(JsonConvert.SerializeObject(response))));
            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);

            var organizationMembershipResource = new OrganizationMembershipResource(_apiClient.Object);

            // When
            var result = await organizationMembershipResource.PostAsync(request);

            // Then
            Assert.Equal(response, result);
        }
    }
}
