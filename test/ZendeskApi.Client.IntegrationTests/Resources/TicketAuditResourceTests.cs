using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketAuditResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketAuditResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorVariantPagination_ShouldReturnTicketAudits()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .TicketAudits.GetAllAsync(new CursorPagerVariant());

            Assert.NotNull(results);
            Assert.Equal(100, results.Count());
        }
    }
}