using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterArticlesResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterArticlesResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllAsync("en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategory_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByCategoryIdAsync("en-gb", 200523402);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithSection_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllBySectionIdAsync("en-gb", 115000024785);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithUser_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByUserIdAsync("en-gb", 485750562);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnSection()
        {
            var client = _clientFactory.GetClient();

            var article = await client
                .HelpCenter
                .Articles
                .GetAsync("en-gb", 203576272);

            Assert.NotNull(article);
        }
    }
}