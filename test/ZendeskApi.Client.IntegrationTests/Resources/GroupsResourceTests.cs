using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class GroupsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public GroupsResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllAsync(new CursorPager());

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllByUserIdAsync(489650852, new CursorPager());

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAllByAssignableAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllByAssignableAsync(new CursorPager());

            Assert.NotNull(results);
        }
    }
}