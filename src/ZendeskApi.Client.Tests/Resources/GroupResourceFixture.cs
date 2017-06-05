using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Tests.Resources
{
    public class GroupResourceFixture
    {
        private Mock<IZendeskApiClient> _apiClient;
        private Mock<HttpClient> _httpClient;

        public GroupResourceFixture()
        {
            _apiClient = new Mock<IZendeskApiClient>();
            _httpClient = new Mock<HttpClient>();
        }

        [Fact]
        public async Task GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);

            var groupResource = new GroupsResource(_apiClient.Object);

            // When
            await groupResource.GetAsync(321);

            // Then
            _apiClient.Verify(c => c.CreateClient("/api/v2/groups"), "");
            _httpClient.Verify(c => c.GetAsync("321"), "");
        }
        
        [Fact]
        public async Task GetAsync_Called_ReturnsResponse()
        {
            // Given
            var response = new GroupResponse { Item = new Group { Id = 1 } };
            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
            _httpClient.Setup(b => b.GetAsync(It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(message));

            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);
            
            var groupResource = new GroupsResource(_apiClient.Object);

            // When
            var result = await groupResource.GetAsync(321);

            // Then
            Assert.Equal(response, result);
        }
    }
}
