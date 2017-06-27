using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketCommentsResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketCommentsResource _resource;
        private readonly TicketsResource _ticketResource;

        public TicketCommentsResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new TicketResourceSampleSite(resource));
            _resource = new TicketCommentsResource(_client, NullLogger.Instance);
            _ticketResource = new TicketsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldGetAllCommentsForTicket() {
            var ticket = await _ticketResource.CreateAsync(new Models.Ticket { Subject = "Test 1" });

            var comments = await _resource.GetAllAsync(ticket.Id.Value);
            Assert.Empty(comments);

            await _resource.AddComment(ticket.Id.Value, new Models.TicketComment { Body = "Hi there! im a comments..." });

            comments = await _resource.GetAllAsync(ticket.Id.Value);
            Assert.Equal(1, comments.Count());
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(0).Body);

            await _resource.AddComment(ticket.Id.Value, new Models.TicketComment { Body = "Hi there! im a second comment..." });

            comments = await _resource.GetAllAsync(ticket.Id.Value);
            Assert.Equal(2, comments.Count());
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(0).Body);
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a second comment...", comments.ElementAt(1).Body);
        }
    }
}
