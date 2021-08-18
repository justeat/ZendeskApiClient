using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public TicketResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
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
    }
}