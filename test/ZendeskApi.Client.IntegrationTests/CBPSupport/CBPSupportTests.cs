using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.CBPSupport
{
    public class CBPSupportTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public CBPSupportTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatableByReplacingTheCursor()
        {
            var client = _clientFactory.GetClient();

            var cursorPager = new CursorPager { Size = 5 };
            var ticketsPageOne = await client
                .Tickets.GetAllAsync(cursorPager);

            Assert.NotNull(ticketsPageOne);
            Assert.Equal(5, ticketsPageOne.Count());
            Assert.True(ticketsPageOne.Meta.HasMore);

            cursorPager.AfterCursor = ticketsPageOne.Meta.AfterCursor;

            var ticketsPageTwo = await client.Tickets.GetAllAsync(cursorPager);
            Assert.NotNull(ticketsPageTwo);
            Assert.Equal(5, ticketsPageTwo.Count());

            var ticketIdsPageOne = ticketsPageOne.Select(ticket => ticket.Id).ToList();
            var ticketIdsPageTwo = ticketsPageTwo.Select(ticket => ticket.Id).ToList();
            Assert.NotEqual(ticketIdsPageOne, ticketIdsPageTwo);
        }

        [Fact]
        public async Task CursorPaginatedIterator_ShouldBePaginatableByCallingNextPage()
        {
            var client = _clientFactory.GetClient();
            var apiClient = _clientFactory.GetApiClient();

            var cursorPager = new CursorPager { Size = 2 };
            var ticketsResponse = await client
                .Tickets.GetAllAsync(cursorPager);

            var iterator = new CursorPaginatedIterator<Ticket>(ticketsResponse, apiClient);
            Assert.True(iterator.HasMore());
            Assert.Equal(2, iterator.Count());
            var ticketIdsPageOne = iterator.Select(ticket => ticket.Id).ToList();
            await iterator.NextPage();
            Assert.Equal(2, iterator.Count());
            var ticketIdsPageTwo = iterator.Select(ticket => ticket.Id).ToList();
            Assert.NotEqual(ticketIdsPageOne, ticketIdsPageTwo);
        }
    }
}
