//using System;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Moq;
//using Newtonsoft.Json;
//using Xunit;
//using ZendeskApi.Client.Resources;
//using ZendeskApi.Contracts.Models;
//using ZendeskApi.Contracts.Requests;
//using ZendeskApi.Contracts.Responses;

//namespace ZendeskApi.Client.Tests.Resources
//{
//    public class OrganizationResourceFixture
//    {
//        private Mock<IZendeskApiClient> _apiClient;
//        private Mock<HttpClient> _httpClient;

//        public OrganizationResourceFixture()
//        {
//            _apiClient = new Mock<IZendeskApiClient>();
//            _httpClient = new Mock<HttpClient>();
//            _apiClient.Setup(b => b.CreateClient(It.IsAny<string>())).Returns(_httpClient.Object);
//        }

//        [Fact]
//        public async Task GetAsync_Called_CallsBuildUriWithFieldId()
//        {
//            // Given
//            var resource = new OrganizationResource(_apiClient.Object);

//            // When
//            await resource.GetAsync(321);

//            // Then
//            _httpClient.Verify(c => c.GetAsync("321"), "");
//        }

//        [Fact]
//        public async Task GetAsync_Called_ReturnsResponse()
//        {
//            // Given
//            var response = new OrganizationResponse { Item = new Organization { Id = 1 } };
//            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
//            _httpClient.Setup(b => b.GetAsync(It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(message));

//            var resource = new OrganizationResource(_apiClient.Object);

//            // When
//            var result = await resource.GetAsync(321);

//            // Then
//            Assert.Equal(response, result);
//        }

//        [Fact]
//        public async Task PutAsync_CalledWithItem_ReturnsReponse()
//        {
//            // Given
//            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
//            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah", Id = 123 } };
//            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
//            _httpClient.Setup(b => b.PutAsync("123", new StringContent(JsonConvert.SerializeObject(request)))).Returns(TaskHelper.CreateTaskFromResult(message));
            
//            var resource = new OrganizationResource(_apiClient.Object);

//            // When
//            var result = await resource.PutAsync(request);

//            // Then
//            Assert.Equal(response, result);
//        }

//        [Fact]
//        public async Task PutAsync_HasNoId_ThrowsException()
//        {
//            // Given
//            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
//            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
//            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
//            _httpClient.Setup(b => b.PutAsync(It.IsAny<string>(), new StringContent(JsonConvert.SerializeObject(request)))).Returns(TaskHelper.CreateTaskFromResult(message));


//            var resource = new OrganizationResource(_apiClient.Object);

//            // When, Then
//            await Assert.ThrowsAsync<ArgumentException>(async () => await resource.PutAsync(request));
//        }
        
//        [Fact]
//        public async Task PostAsync_CalledWithItem_ReturnsReponse()
//        {
//            // Given
//            var response = new OrganizationResponse { Item = new Organization { Name = "blah blah" } };
//            var request = new OrganizationRequest { Item = new Organization { Name = "blah blah" } };
//            var message = new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(response)) };
//            _httpClient.Setup(b => b.PostAsync(It.IsAny<string>(), new StringContent(JsonConvert.SerializeObject(request)))).Returns(TaskHelper.CreateTaskFromResult(message));

//            var resource = new OrganizationResource(_apiClient.Object);

//            // When
//            var result = await resource.PostAsync(request);

//            // Then
//            Assert.Equal(response, result);
//        }

//        [Fact]
//        public async Task DeleteAsync_Called_CallsBuildUriWithFieldId()
//        {
//            // Given
//            var resource = new OrganizationResource(_apiClient.Object);

//            // When
//            await resource.DeleteAsync(321);

//            // Then
//            _httpClient.Verify(c => c.DeleteAsync("321"), "");
//        }
//    }
//}
