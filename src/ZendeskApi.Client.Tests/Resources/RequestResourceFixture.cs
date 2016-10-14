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
    public class RequestResourceFixture
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
            var resource = new RequestResource(_client.Object);

            // When
            resource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new RequestResource(_client.Object);

            // When
            await resource.GetAsync(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Get_Called_ReturnsRequestResponse()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Id = 1 } };
            _client.Setup(b => b.Get<RequestResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var resource = new RequestResource(_client.Object);

            // When
            var result = resource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAsync_Called_ReturnsRequestResponse()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Id = 1 } };
            _client.Setup(b => b.GetAsync<RequestResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var resource = new RequestResource(_client.Object);

            // When
            var result = await resource.GetAsync(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Get_CalledWithStatuses_CallsBuildUriWithStatuses()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new RequestResource(_client.Object);
            var statuses = new List<TicketStatus> {TicketStatus.Hold, TicketStatus.Open};

            // When
            resource.Get(statuses);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("requests")), It.Is<string>(s => s.Contains("status=hold,open"))));
        }

        [Test]
        public async void GetAsync_CalledWithStatuses_CallsBuildUriWithStatuses()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var resource = new RequestResource(_client.Object);
            var statuses = new List<TicketStatus> {TicketStatus.Hold, TicketStatus.Open};

            // When
            await resource.GetAsync(statuses);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("requests")), It.Is<string>(s => s.Contains("status=hold,open"))));
        }

        [Test]
        public void Put_Called_BuildsUri()
        {
            // Given
            var request = new RequestRequest { Item = new Request { Subject = "blah blah", Id = 123 } };
            var resource = new RequestResource(_client.Object);

            // When
            resource.Put(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var request = new RequestRequest { Item = new Request { Subject = "blah blah", Id = 123 } };
            var resource = new RequestResource(_client.Object);

            // When
            await resource.PutAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Put_CalledWithRequest_ReturnsRequestReponse()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah", Id = 123 } };
            _client.Setup(b => b.Put<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var resource = new RequestResource(_client.Object);

            // When
            var result = resource.Put(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PutAsync_CalledWithRequest_ReturnsRequestReponse()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah", Id = 123 } };
            _client.Setup(b => b.PutAsync<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));
            var resource = new RequestResource(_client.Object);

            // When
            var result = await resource.PutAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_RequestHasNoId_ThrowsException()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            _client.Setup(b => b.Put<RequestResponse>(
                It.IsAny<Uri>(),
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var requestResource = new RequestResource(_client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(() => requestResource.Put(request));
        }

        [Test]
        public void PutAsync_RequestHasNoId_ThrowsException()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            _client.Setup(b => b.PutAsync<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var requestResource = new RequestResource(_client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(async () => await requestResource.PutAsync(request));
        }

        [Test]
        public void Post_Called_BuildsUri()
        {
            // Given
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            var requestResource = new RequestResource(_client.Object);
            
            // When
            requestResource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            var requestResource = new RequestResource(_client.Object);

            // When
            await requestResource.PostAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Post_CalledWithRequest_ReturnsRequestReponse()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            _client.Setup(b => b.Post<RequestResponse>(
                It.IsAny<Uri>(), 
                request,
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var requestResource = new RequestResource(_client.Object);

            // When
            var result = requestResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PostAsync_CalledWithRequest_ReturnsRequestReponse()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Subject = "blah blah" } };
            var request = new RequestRequest { Item = new Request { Subject = "blah blah" } };
            _client.Setup(b => b.PostAsync<RequestResponse>(
                It.IsAny<Uri>(), 
                request, 
                "application/json",
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(TaskHelper.CreateTaskFromResult(response));

            var requestResource = new RequestResource(_client.Object);

            // When
            var result = await requestResource.PostAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Delete_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var requestResource = new RequestResource(_client.Object);

            // When
            requestResource.Delete(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public async void DeleteAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var requestResource = new RequestResource(_client.Object);

            // When
            await requestResource.DeleteAsync(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Delete_Called_CallsDeleteOnClient()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Id = 1 } };
            _client.Setup(b => b.Get<RequestResponse>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Returns(response);

            var requestResource = new RequestResource(_client.Object);

            // When
            requestResource.Delete(321);

            // Then
            _client.Verify(c => c.Delete<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async void DeleteAsync_Called_CallsDeleteOnClient()
        {
            // Given
            var response = new RequestResponse { Item = new Request { Id = 1 } };
            _client.Setup(b => b.GetAsync<RequestResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var requestResource = new RequestResource(_client.Object);

            // When
            await requestResource.DeleteAsync(321);

            // Then
            _client.Verify(c => c.DeleteAsync<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
