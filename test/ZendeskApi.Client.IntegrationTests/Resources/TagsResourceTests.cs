using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TagsResourceTest : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TagsResourceTest(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatable()
        {
            var client = _clientFactory.GetClient();

            var cursorPager = new CursorPager { Size = 5 };
            var tagsPageOne = await client
                .Tags.GetAllAsync(cursorPager);

            Assert.NotNull(tagsPageOne);
            Assert.Equal(5, tagsPageOne.Count());
            Assert.True(tagsPageOne.Meta.HasMore);

            cursorPager.AfterCursor = tagsPageOne.Meta.AfterCursor;

            var tagsPageTwo = await client.Tags.GetAllAsync(cursorPager);
            Assert.NotNull(tagsPageTwo);
            Assert.Equal(5, tagsPageTwo.Count());

            var tagIdsPageOne = tagsPageOne.Select(tag => tag.Name).ToList();
            var tagIdsPageTwo = tagsPageTwo.Select(tag => tag.Name).ToList();
            Assert.NotEqual(tagIdsPageOne, tagIdsPageTwo);
        }
    }
}