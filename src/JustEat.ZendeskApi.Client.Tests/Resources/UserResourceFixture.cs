﻿using System;
using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
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
    }
}
