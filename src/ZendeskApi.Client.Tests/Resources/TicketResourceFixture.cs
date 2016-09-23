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
    public class TicketResourceFixture
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
            var ticketResource = new TicketResource(_client.Object);

            // When
            ticketResource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public async void GetAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(_client.Object);

            // When
            await ticketResource.GetAsync(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Get_Called_ReturnsTicketResponse()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Id = 1 }};
            _client.Setup(b => b.Get<TicketResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = ticketResource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAsync_Called_ReturnsTicketResponse()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Id = 1 }};
            _client.Setup(b => b.GetAsync<TicketResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = await ticketResource.GetAsync(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void GetAll_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(_client.Object);

            // When
            ticketResource.GetAll(new List<long> { 321, 456, 789 });

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/show_many")), It.IsAny<string>()));
        }

        [Test]
        public async void GetAllAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.IsAny<string>())).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(_client.Object);

            // When
            await ticketResource.GetAllAsync(new List<long> { 321, 456, 789 });

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("/show_many")), It.IsAny<string>()));
        }

        [Test]
        public void GetAll_Called_ReturnsTicketResponse()
        {
            // Given
            var response = new TicketListResponse { Results = new List<Ticket> { new Ticket { Id = 1 } } };
            _client.Setup(b => b.Get<TicketListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = ticketResource.GetAll(new List<long> { 321, 456, 789 });

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void GetAllAsync_Called_ReturnsTicketResponse()
        {
            // Given
            var response = new TicketListResponse { Results = new List<Ticket> { new Ticket { Id = 1 } } };
            _client.Setup(b => b.GetAsync<TicketListResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = await ticketResource.GetAllAsync(new List<long> { 321, 456, 789 });

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_Called_BuildsUri()
        {
            // Given
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah", Id = 123 } };
            var ticketResource = new TicketResource(_client.Object);

            // When
            ticketResource.Put(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PutAsync_Called_BuildsUri()
        {
            // Given
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah", Id = 123 } };
            var ticketResource = new TicketResource(_client.Object);

            // When
            await ticketResource.PutAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Put_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah", Id = 123 } };
            _client.Setup(b => b.Put<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = ticketResource.Put(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PutAsync_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah", Id = 123 } };
            _client.Setup(b => b.PutAsync<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = await ticketResource.PutAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Put_TicketHasNoId_ThrowsException()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            _client.Setup(b => b.Put<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(() => ticketResource.Put(request));
        }

        [Test]
        public void PutAsync_TicketHasNoId_ThrowsException()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            _client.Setup(b => b.PutAsync<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(_client.Object);

            // When, Then
            Assert.Throws<ArgumentException>(async () => await ticketResource.PutAsync(request));
        }

        [Test]
        public void Post_Called_BuildsUri()
        {
            // Given
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            var ticketResource = new TicketResource(_client.Object);
            
            // When
            ticketResource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public async void PostAsync_Called_BuildsUri()
        {
            // Given
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            var ticketResource = new TicketResource(_client.Object);

            // When
            await ticketResource.PostAsync(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Post_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            _client.Setup(b => b.Post<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = ticketResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public async void PostAsync_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Subject = "blah blah" } };
            var request = new TicketRequest { Item = new Ticket { Subject = "blah blah" } };
            _client.Setup(b => b.PostAsync<TicketResponse>(It.IsAny<Uri>(), request, "application/json", It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = await ticketResource.PostAsync(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Delete_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(_client.Object);

            // When
            ticketResource.Delete(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public async void DeleteAsync_Called_CallsBuildUriWithFieldId()
        {
            // Given
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(_client.Object);

            // When
            await ticketResource.DeleteAsync(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Delete_Called_CallsDeleteOnClient()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Id = 1 } };
            _client.Setup(b => b.Get<TicketResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            ticketResource.Delete(321);

            // Then
            _client.Verify(c => c.Delete<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async void DeleteAsync_Called_CallsDeleteOnClient()
        {
            // Given
            var response = new TicketResponse { Item = new Ticket { Id = 1 } };
            _client.Setup(b => b.GetAsync<TicketResponse>(It.IsAny<Uri>(), It.IsAny<string>(), It.IsAny<string>())).Returns(TaskHelper.CreateTaskFromResult(response));
            var ticketResource = new TicketResource(_client.Object);

            // When
            await ticketResource.DeleteAsync(321);

            // Then
            _client.Verify(c => c.DeleteAsync<object>(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
