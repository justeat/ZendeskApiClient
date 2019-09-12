using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterSectionsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterSectionsResourceTests(
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

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync("en-gb");

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategory_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync(200506022, "en-gb");

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnSection()
        {
            var client = _clientFactory.GetClient();

            var section = await client
                .HelpCenter
                .Sections
                .GetAsync(115000024785, "en-gb");

            Assert.NotNull(section);
        }
    }
}