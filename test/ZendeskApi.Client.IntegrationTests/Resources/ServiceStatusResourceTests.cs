using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class ServiceStatusResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        private const string Subdomain = "<your subdomain>";

        public ServiceStatusResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task ListComponents_WhenCalled_ShouldReturnComponents()
        {
            var client = _clientFactory.GetClient();

            var components = await client
                .ServiceStatus
                .ListComponents(Subdomain);

            Assert.NotEmpty(components);
        }

        [Fact]
        public async Task ListSubComponents_WhenCalled_ShouldReturnSubComponents()
        {
            var client = _clientFactory.GetClient();

            var components = await client
                .ServiceStatus
                .ListSubComponents("support", Subdomain);

            Assert.NotEmpty(components);
        }

        [Fact]
        public async Task GetComponentStatus_WhenCalled_ShouldReturnStatus()
        {
            var client = _clientFactory.GetClient();

            var status = await client
                .ServiceStatus
                .GetComponentStatus("support", Subdomain);

            Assert.NotNull(status);
        }

        [Fact]
        public async Task GetSubComponentStatus_WhenCalled_ShouldReturnStatus()
        {
            var client = _clientFactory.GetClient();

            var status = await client
                .ServiceStatus
                .GetSubComponentStatus("support", "ticketing", Subdomain);

            Assert.NotNull(status);
        }
    }
}
