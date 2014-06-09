using System;
using System.Collections.Generic;
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
        [Test]
        public void Get_Called_ReturnsTicketResource()
        {
            // Given
            var response = new TicketResponse {Tickets = new List<Ticket> { new Ticket { Id = 1 }}};
            var uri = new Uri("https://justeatukpoc1399564481.zendesk.com/api/v2/search.json?query=type:ticket&restaurantid=321");
            var baseClient = new Mock<IBaseClient>();
            baseClient.Setup(b => b.BuildUri(It.IsAny<string>(), It.Is<string>(s => s.Contains("321")))).Returns(uri);
            baseClient.Setup(b => b.Get<TicketResponse>(uri))
                .Returns(response);
            var ticketResource = new TicketResource(baseClient.Object);

            // When
            var result = ticketResource.Get(321);

            // Then
            Assert.That(result, Is.EqualTo(response));

        }
    }
}
