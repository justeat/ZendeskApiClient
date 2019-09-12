using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterCategoriesResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterCategoriesResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnCategories()
        {
            var client = _clientFactory.GetClient();

            var categories = await client
                .HelpCenter
                .Categories
                .GetAllAsync("en-gb");

            Assert.NotEmpty(categories);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnCategory()
        {
            var client = _clientFactory.GetClient();

            var category = await client
                .HelpCenter
                .Categories
                .GetAsync("en-gb", 200523402);

            Assert.NotNull(category);
        }
    }
}
