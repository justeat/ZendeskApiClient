using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

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
        public async Task GetAllAsync_WhenCalled_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllAsync("en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursor_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllAsync(new CursorPager{Size = 100}, "en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategory_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByCategoryIdAsync(360003544498, "en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategoryIdAndWithCursorPagination_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByCategoryIdAsync(360003544498, new CursorPager(), "en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithSection_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllBySectionIdAsync(360005462017, "en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithSectionAndWithCursorPagination_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllBySectionIdAsync(360005462017, new CursorPager(), "en-gb");

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithUser_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByUserIdAsync(794337992);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithUserAndWithCursorPagination_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByUserIdAsync(794337992, new CursorPager());

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnArticle()
        {
            var client = _clientFactory.GetClient();

            var article = await client
                .HelpCenter
                .Articles
                .GetAsync(4402330216721, "en-gb");

            Assert.NotNull(article);
        }
    }
}