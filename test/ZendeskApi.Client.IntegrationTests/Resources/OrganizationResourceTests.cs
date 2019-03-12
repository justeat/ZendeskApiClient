using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.IntegrationTests.Settings;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class OrganizationResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public OrganizationResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task CreateOrganisation()
        {
            var settings = new ZendeskSettings();

            _output.WriteLine($"url: {settings.Url}");

            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var found = await client
                .Organizations
                .GetAllByExternalIdsAsync(new[]
                {
                    id
                });

            Assert.Single(found);

            var org = found.First();

            Assert.Equal(id, org.ExternalId);
            Assert.Equal($"ZendeskApi.Client.IntegrationTests {id}", org.Name);

            await client.Organizations
                .DeleteAsync(created.Id);
        }
    }
}
