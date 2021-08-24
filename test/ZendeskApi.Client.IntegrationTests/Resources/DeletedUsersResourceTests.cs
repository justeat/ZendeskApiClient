using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class DeletedUsersResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public DeletedUsersResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnDeletedUsers()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .DeletedUsers.GetAllAsync(new CursorPager());

            Assert.NotNull(results);
        }
    }
}