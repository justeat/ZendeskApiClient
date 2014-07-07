using System;
using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Queries;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class UserResourceFixture
    {
        private Mock<IBaseClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IBaseClient>();
        }

        [Test]
        public void TestPostUser()
        {
            var client = new ZendeskClient(new Uri("https://justeatukpoc1399564481.zendesk.com"),
                new ZendeskDefaultConfiguration("zendeskapi@just-eat.com", "R8CvArGs3sOWoK0muPh8r39XocxylQEUwV2jFL0a"));
            //var request = new UserRequest { Item = new User { Name = "Test Spike User 3", Email = "test@email.com", Phone = "12345678", OrganizationId = 30645171, Verified = true } };
            //var result = client.Users.Post(request);

            //var result = client.Organizations.GetAll();

            //var result = client.Search.Find(new ZendeskQuery<ListResponse<Organization>>().WithPaging(2, 100));

            var result = client.Search.Find(new ZendeskQuery<Organization>().WithPaging(1,20000));
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
        public void Get_Called_ReturnsUserResponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Id = 1 }};
            _client.Setup(b => b.Get<UserResponse>(It.IsAny<Uri>())).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.Get(4321);

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
        public void GetAll_Called_ReturnsUserResponse()
        {
            // Given
            var response = new UserListResponse { Results = new List<User> { new User { Id = 1 } } };
            _client.Setup(b => b.Get<UserListResponse>(It.IsAny<Uri>())).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.GetAll(new List<long> { 4321, 3456, 6789 });

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
        public void Post_CalledWithUser_ReturnsUserReponse()
        {
            // Given
            var response = new UserResponse { Item = new User { Name = "Owner Name" } };
            var request = new UserRequest { Item = new User { Name = "Owner Name" } };
            _client.Setup(b => b.Post<UserResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var userResource = new UserResource(_client.Object);

            // When
            var result = userResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
