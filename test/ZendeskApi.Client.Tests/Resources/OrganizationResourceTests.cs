using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationResourceTests
    {
        private readonly OrganizationsResource _resource;

        public OrganizationResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient(resource => new OrganizationResourceSampleSite(resource));
            _resource = new OrganizationsResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldGetOrganization()
        {
            var org = await _resource.CreateAsync(new Organization
            {
                Name = "OrgName",
                ExternalId = "123"
            });

            var org2 = await _resource.GetAsync(org.Id);

            Assert.Equal(JsonConvert.SerializeObject(org), JsonConvert.SerializeObject(org2));
        }

        [Fact]
        public async Task ShouldCreateOrganization()
        {
            var org = await _resource.CreateAsync(new Organization
            {
                Name = "OrgName",
                ExternalId = "123"
            });

            Assert.Equal("OrgName", org.Name);
            Assert.Equal("123", org.ExternalId);
        }
    }
}
