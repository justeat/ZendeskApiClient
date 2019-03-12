using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Settings;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class OrganizationResourceTests
    {
        [Fact]
        public async Task CreateOrganisation()
        {
            var client = GetClient();

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

        private IZendeskClient GetClient()
        {
            var settings = new ZendeskSettings();

            return new ZendeskClient(
                new ZendeskApiClient(
                    new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
                    {
                        EndpointUri = settings.Url,
                        Username = settings.Username,
                        Token = settings.Token
                    })
                )
            );
        }
    }
}
