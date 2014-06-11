using System;
using System.Collections.Generic;
using System.Text;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Resources;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;
using Moq;
using NUnit.Framework;

namespace JustEat.ZendeskApi.Client.Tests.Resources
{
    public class TicketResourceFixture
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
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(new Uri("http://search"));
            var ticketResource = new TicketResource(_client.Object);

            // When
            ticketResource.Get(321);

            // Then
            _client.Verify(c => c.BuildUri(It.Is<string>(s => s.Contains("321")), ""));
        }

        [Test]
        public void Get_Called_ReturnsTicketResponse()
        {
            // Given
            var response = new TicketResponse { Entity = new Ticket { Id = 1 }};
            _client.Setup(b => b.Get<TicketResponse>(It.IsAny<Uri>())).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = ticketResource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        public void Post_Called_BuildsUri()
        {
            // Given
            var request = new TicketRequest { Entity = new Ticket  {Subject = "blah blah"}};
            var ticketResource = new TicketResource(_client.Object);
            
            // When
            ticketResource.Post(request);

            // Then
            _client.Setup(b => b.BuildUri(It.IsAny<string>(), ""));
        }

        [Test]
        public void Post_CalledWithTicket_ReturnsTicketReponse()
        {
            // Given
            var response = new TicketResponse { Entity = new Ticket  {Subject = "blah blah"}};
            var request = new TicketRequest { Entity = new Ticket { Subject = "blah blah" } };
            _client.Setup(b => b.Post<TicketResponse>(It.IsAny<Uri>(), request, "application/json")).Returns(response);
            var ticketResource = new TicketResource(_client.Object);

            // When
            var result = ticketResource.Post(request);

            // Then
            Assert.That(result, Is.EqualTo(response));
        }
    }
}
