using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Models.Responses;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

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
        public async Task ShouldGetAllCommentsForTicket()
        {
            var ticket = new TicketResponse(); // await _ticketResource.CreateAsync(new TicketResponse { /*Subject = "Test 1"*/ });

            var comments = await _resource.GetAllAsync(ticket.Id);
            Assert.Empty(comments);

            await _resource.AddComment(ticket.Id, new Models.TicketComment { Body = "Hi there! im a comments..." });

            comments = await _resource.GetAllAsync(ticket.Id);
            Assert.Equal(1, comments.Count());
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(0).Body);

            await _resource.AddComment(ticket.Id, new Models.TicketComment { Body = "Hi there! im a second comment..." });

            comments = await _resource.GetAllAsync(ticket.Id);
            Assert.Equal(2, comments.Count());
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(0).Body);
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a second comment...", comments.ElementAt(1).Body);
        }
    }
}
