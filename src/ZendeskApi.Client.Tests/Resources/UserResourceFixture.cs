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
    public class UserResourceFixture
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
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("4321")))).Returns(new Uri("http://search"));
            var userResource = new UserResource(_client.Object);

            // When
            userResource.Get(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("4321")), ""));
        }

        [Test]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("4321")))).Returns(new Uri("http://search"));
            var userResource = new UserResource(_client.Object);

            // When
            await userResource.GetAsync(4321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("4321")), ""));
        }

        [Test]
        public void Get_Called_ReturnsUserResponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Id = 1 } };
            _client.Setup(b => b.Get<UserResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.Get(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAsync_Called_ReturnsUserResponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Id = 1 } };
            _client.Setup(b => b.GetAsync<UserResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(_client.Object);

            // When
            var result = await userResource.GetAsync(4321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void GetAll_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var userResource = new UserResource(_client.Object);

            // When
            userResource.GetAll(new List<long> { 4321, 3456, 6789 });

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/show_many")), It.Is<string>(st => st.Contains("4321,3456,6789"))));
        }

        [Test]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var userResource = new UserResource(_client.Object);

            // When
            await userResource.GetAllAsync(new List<long> { 4321, 3456, 6789 });

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/show_many")), It.Is<string>(st => st.Contains("4321,3456,6789"))));
        }

        [Test]
        public void GetAll_Called_ReturnsUserResponse()
        {
            // Given
            var response = new UserListResponse { Results = new List<User> { new User { Id = 1 } } };
            _client.Setup(b => b.Get<UserListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.GetAll(new List<long> { 4321, 3456, 6789 });

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAllAsync_Called_ReturnsUserResponse()
        {
            // Given
            var response = new UserListResponse { Results = new List<User> { new User { Id = 1 } } };
            _client.Setup(b => b.GetAsync<UserListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(_client.Object);

            // When
            var result = await userResource.GetAllAsync(new List<long> { 4321, 3456, 6789 });

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Post_Called_BuildsUri()
        {
            // Given
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            var userResource = new UserResource(_client.Object);

            // When
            userResource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            var userResource = new UserResource(_client.Object);

            // When
            await userResource.PostAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Post_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            _client.Setup(b => b.Post<UserResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PostAsync_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            _client.Setup(b => b.PostAsync<UserResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(_client.Object);

            // When
            var result = await userResource.PostAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_Called_BuildsUri()
        {
            // Given
            var request = new UserRequest { Item = new User { Name = "Owner Name", Id = 123 } };
            var userResource = new UserResource(_client.Object);

            // When
            userResource.Put(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var request = new UserRequest { Item = new User { Name = "Owner Name", Id = 123 } };
            var userResource = new UserResource(_client.Object);

            // When
            await userResource.PutAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Put_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name", Id = 123 } };
            _client.Setup(b => b.Put<UserResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.Put(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PutAsync_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name", Id = 123 } };
            _client.Setup(b => b.PutAsync<UserResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var userResource = new UserResource(_client.Object);

            // When
            var result = await userResource.PutAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
