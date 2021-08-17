using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class DeletedTicketsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public DeletedTicketsResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }


        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnDeletedTickets()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .DeletedTickets.GetAllAsync(new CursorPager{ Size = 10 });

            Assert.NotNull(results);
            Assert.Equal(10, results.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithQueryWithCursorPagination_ShouldReturnDeletedTickets()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .DeletedTickets.GetAllAsync(query => { }, new CursorPager { Size = 10 });

            Assert.NotNull(results);
            Assert.Equal(10, results.Count());
        }
    }
}