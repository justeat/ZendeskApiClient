using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

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
            long? userId = null;

            try
            {
                var user = await client.Users.CreateAsync(new UserCreateRequest($"{typeof(GroupsResourceTests).FullName}-user"));
                userId = user.Id;

                var group = await client.Groups.CreateAsync(new GroupCreateRequest($"{typeof(GroupsResourceTests).FullName}-group"));
                await client.Users.UpdateAsync(new UserUpdateRequest(userId.Value)
                {
                    DefaultGroupId = group.Id
                });

                var results = await client
                    .Groups.GetAllByUserIdAsync(userId.Value, new CursorPager());

                Assert.NotNull(results);
            }
            finally
            {
                if (userId.HasValue)
                {
                    await client.Users.DeleteAsync(userId.Value);
                }
            }
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