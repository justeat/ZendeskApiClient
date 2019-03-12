using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{   public class TicketCommentsResourceTests
    {
        private readonly TicketCommentsResource _resource;
        private readonly TicketsResource _ticketResource;

        public TicketCommentsResourceTests()
        {
            var client = new DisposableZendeskApiClient(resource => new TicketResourceSampleSite(resource));
            _resource = new TicketCommentsResource(client, NullLogger.Instance);
            _ticketResource = new TicketsResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldGetAllCommentsForTicket()
        {
            var ticket = await _ticketResource.CreateAsync(new TicketCreateRequest("description") { Subject = "Test 1" });

            var comments = await _resource.ListAsync(ticket.Ticket.Id);
            Assert.Single(comments);
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("description", comments.ElementAt(0).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a comments..." });

            comments = await _resource.ListAsync(ticket.Ticket.Id);
            Assert.Equal(2, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a second comment..." });

            comments = await _resource.ListAsync(ticket.Ticket.Id);
            Assert.Equal(3, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);
            Assert.NotNull(comments.ElementAt(2).Id);
            Assert.Equal("Hi there! im a second comment...", comments.ElementAt(2).Body);
        }
    }
}
