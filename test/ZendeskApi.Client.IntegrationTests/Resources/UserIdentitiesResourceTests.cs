using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class UserIdentitiesResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public UserIdentitiesResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnUserIdentities()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .UserIdentities.GetAllByUserIdAsync(485750562, new CursorPager());

            Assert.NotNull(results);
        }
    }
}