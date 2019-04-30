using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

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
        public async Task GetAllAsync_WhenCalled_ReturnsOrganisations()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var organisations = await client
                .Organizations
                .GetAllAsync();

            Assert.NotEmpty(organisations);

            await client.Organizations
                .DeleteAsync(created.Id);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOrganisationIds_ReturnsOrganisations()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var organisations = await client
                .Organizations
                .GetAllAsync(new []
                {
                    created.Id
                });

            Assert.NotEmpty(organisations);

            await client.Organizations
                .DeleteAsync(created.Id);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithExternalIds_ReturnsOrganisations()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var organisations = await client
                .Organizations
                .GetAllByExternalIdsAsync(new[]
                {
                    created.ExternalId
                });

            Assert.NotEmpty(organisations);

            await client.Organizations
                .DeleteAsync(created.Id);
        }

        [Fact]
        public async Task GetAllByUserId_WhenCalled_ReturnsOrganisations()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var createdOrganisation = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var userId = Guid.NewGuid().ToString();

            var user = await client.Users
                .CreateAsync(new UserCreateRequest(userId));

            var organisationResultsBeforeMembership = await client
                .Organizations
                .GetAllByUserIdAsync(user.Id);

            Assert.Empty(organisationResultsBeforeMembership);

            var organisationMembership = await client
                .OrganizationMemberships
                .CreateAsync(new OrganizationMembership
                {
                    OrganizationId = createdOrganisation.Id,
                    UserId = user.Id
                });

            var organisationResultsAfterMembership = await client
                .Organizations
                .GetAllByUserIdAsync(user.Id);

            Assert.NotEmpty(organisationResultsAfterMembership);

            await client
                .OrganizationMemberships
                .DeleteAsync(organisationMembership.Id);

            await client
                .Users
                .DeleteAsync(user.Id);

            await client
                .Organizations
                .DeleteAsync(createdOrganisation.Id);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ReturnsOrganisation()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var organisation = await client
                .Organizations
                .GetAsync(created.Id);

            Assert.NotNull(organisation);

            await client.Organizations
                .DeleteAsync(created.Id);
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithInvalidId_ReturnsNull()
        {
            var client = _clientFactory.GetClient();

            var organisation = await client
                .Organizations
                .GetAsync(long.MaxValue);

            Assert.Null(organisation);
        }

        [Fact]
        public async Task CreateAync_WhenCalled_CreatesOrganisation()
        {
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

        [Fact]
        public async Task UpdateAsync_WhenCalled_UpdatesOrganisation()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();
            var updatedId = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var updated = await client.Organizations
                .UpdateAsync(new Organization
                {
                    Id = created.Id,
                    Name = $"ZendeskApi.Client.IntegrationTests {updatedId}",
                    ExternalId = updatedId
                });

            Assert.NotNull(updated);

            var found = await client
                .Organizations
                .GetAllByExternalIdsAsync(new[]
                {
                    updatedId
                });

            Assert.Single(found);

            var org = found.First();

            Assert.Equal(updatedId, org.ExternalId);
            Assert.Equal($"ZendeskApi.Client.IntegrationTests {updatedId}", org.Name);

            await client.Organizations
                .DeleteAsync(created.Id);
        }

        [Fact]
        public async Task UpdateAsync_WhenCalledWithInvalidId_ReturnsNull()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var updated = await client.Organizations
                .UpdateAsync(new Organization
                {
                    Id = long.MaxValue,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}",
                    ExternalId = id
                });

            Assert.Null(updated);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_DeletesOrganisation()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid().ToString();

            var created = await client.Organizations
                .CreateAsync(new Organization
                {
                    ExternalId = id,
                    Name = $"ZendeskApi.Client.IntegrationTests {id}"
                });

            var beforeDeleteSearch = await client
                .Organizations
                .GetAllByExternalIdsAsync(new[]
                {
                    id
                });

            Assert.Single(beforeDeleteSearch);

            await client.Organizations
                .DeleteAsync(created.Id);

            var afterDeleteSearch = await client
                .Organizations
                .GetAllByExternalIdsAsync(new[]
                {
                    id
                });

            Assert.Empty(afterDeleteSearch);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithInvalidId_Throws()
        {
            var client = _clientFactory.GetClient();

            await Assert.ThrowsAsync<HttpRequestException>(() => client.Organizations
                .DeleteAsync(long.MaxValue));
        }
    }
}
