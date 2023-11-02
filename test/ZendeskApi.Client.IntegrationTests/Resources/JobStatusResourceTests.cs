using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class JobStatusResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public JobStatusResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatable()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .JobStatuses.GetAllAsync(new CursorPager()
                {
                    Size = 2
                });
            var iterator = new CursorPaginatedIterator<JobStatusResponse>(results, client);

            Assert.NotNull(results);
            Assert.Equal(2, iterator.Count());

            await iterator.NextPage();
            Assert.True(iterator.Count() <= 2);
        }

    }
}