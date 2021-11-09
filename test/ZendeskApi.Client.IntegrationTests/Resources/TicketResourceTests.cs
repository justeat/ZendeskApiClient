using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnTickets()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Tickets.GetAllAsync(new CursorPager());

            Assert.NotNull(results);
            Assert.Equal(100, results.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnSpecificAmountOfTickets()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Tickets.GetAllAsync(new CursorPager()
                {
                    Size=10
                });

            Assert.NotNull(results);
            Assert.Equal(10, results.Count());
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenCalledWithCursorPagination_ShouldReturnTickets()
        {
            var client = _clientFactory.GetClient();
            TicketResponse createdTicket = null;
            try
            {
                createdTicket = await client.Tickets.CreateAsync(new Requests.TicketCreateRequest()
                {
                    Comment = new TicketComment() { Body = "Printer is on fire" },
                    OrganisationId = 360195486037,
                });
                var results = (TicketsListCursorResponse)await client
                    .Tickets.GetAllByOrganizationIdAsync(360195486037, new CursorPager());

                Assert.NotNull(results);
            }
            finally
            {
                if (createdTicket != null)
                    await client.Tickets.DeleteAsync(createdTicket.Ticket.Id);
            }
        }
    }
}