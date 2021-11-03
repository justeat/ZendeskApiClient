using System.Linq;
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
            await client.UserIdentities.CreateUserIdentityAsync(new UserIdentity(){
                Type="twitter",
                Value="handle"
            }, 368420617118);

            var results = await client
                .UserIdentities.GetAllByUserIdAsync(368420617118, new CursorPager());

            Assert.NotNull(results);

            await client.UserIdentities.DeleteAsync(368420617118, (long)results.First().Id);
        }
    }
}