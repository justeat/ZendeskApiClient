using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class OrganizationMembershipsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public OrganizationMembershipsResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnMemberships()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            var memberships = await client
                .OrganizationMemberships
                .GetAllAsync();

            Assert.NotEmpty(memberships);

            await TeardownAndDeleteMembership(client, setup);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalled_ShouldReturnMemberships()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            var memberships = await client
                .OrganizationMemberships
                .GetAllForUserAsync(setup.User.Id);

            Assert.NotEmpty(memberships);

            await TeardownAndDeleteMembership(client, setup);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnMembership()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            var membership = await client
                .OrganizationMemberships
                .GetAsync(setup.OrganizationMembership.Id);

            Assert.NotNull(membership);

            await TeardownAndDeleteMembership(client, setup);
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithInvalidId_ShouldReturnNull()
        {
            var client = _clientFactory.GetClient();

            var membership = await client
                .OrganizationMemberships
                .GetAsync(long.MaxValue);

            Assert.Null(membership);
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenCalled_ShouldReturnMembership()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            var membership = await client
                .OrganizationMemberships
                .GetForUserAndOrganizationAsync(setup.User.Id, setup.OrganizationMembership.Id);

            Assert.NotNull(membership);

            await TeardownAndDeleteMembership(client, setup);
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenCalledWithInvalidId_ShouldReturnNull()
        {
            var client = _clientFactory.GetClient();

            var membership = await client
                .OrganizationMemberships
                .GetForUserAndOrganizationAsync(long.MaxValue, long.MaxValue);

            Assert.Null(membership);
        }

        [Fact]
        public async Task GetAllForOrganizationAsync_WhenCalled_ShouldReturnMemberships()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            var memberships = await client
                .OrganizationMemberships
                .GetAllForOrganizationAsync(setup.Organization.Id);

            Assert.NotEmpty(memberships);

            await TeardownAndDeleteMembership(client, setup);
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreateMembership()
        {
            var client = _clientFactory.GetClient();

            var setup = await Setup(client);

            var createdOrganisationMembership = await client
                .OrganizationMemberships
                .CreateAsync(new OrganizationMembership
                {
                    OrganizationId = setup.Organization.Id,
                    UserId = setup.User.Id
                });

            var getOrganisationMembership = await client
                .OrganizationMemberships
                .GetAsync(createdOrganisationMembership.Id);

            Assert.NotNull(getOrganisationMembership);

            await client
                .OrganizationMemberships
                .DeleteAsync(createdOrganisationMembership.Id);

            await Teardown(client, setup);
        }

        [Fact]
        public async Task PostForUserAsync_WhenCalled_ShouldCreateMembership()
        {
            var client = _clientFactory.GetClient();

            var setup = await Setup(client);

            var createdOrganisationMembership = await client
                .OrganizationMemberships
                .PostForUserAsync(new OrganizationMembership
                {
                    OrganizationId = setup.Organization.Id,
                    UserId = setup.User.Id
                }, setup.User.Id);

            var getOrganisationMembership = await client
                .OrganizationMemberships
                .GetAsync(createdOrganisationMembership.Id);

            Assert.NotNull(getOrganisationMembership);

            await client
                .OrganizationMemberships
                .DeleteAsync(createdOrganisationMembership.Id);

            await Teardown(client, setup);
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreateManyMemberships()
        {
            var client = _clientFactory.GetClient();

            var setupOne = await Setup(client);
            var setupTwo = await Setup(client);

            var jobStatus = await client
                .OrganizationMemberships
                .CreateAsync(new []
                {
                    new OrganizationMembership
                    {
                        OrganizationId = setupOne.Organization.Id,
                        UserId = setupOne.User.Id
                    },
                    new OrganizationMembership
                    {
                        OrganizationId = setupTwo.Organization.Id,
                        UserId = setupTwo.User.Id
                    }
                });

            Assert.NotNull(jobStatus);

            var membershipOne = await client
                .OrganizationMemberships
                .GetAllForOrganizationAsync(setupOne.Organization.Id);

            var membershipTwo = await client
                .OrganizationMemberships
                .GetAllForOrganizationAsync(setupOne.Organization.Id);

            try
            {
                foreach (var membership in membershipOne.Concat(membershipTwo).Select(x => x.Id))
                {
                    await client
                        .OrganizationMemberships
                        .DeleteAsync(membership);
                }
            }
            catch
            { }

            await Teardown(client, setupOne);
            await Teardown(client, setupTwo);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithId_ShouldDeleteMembership()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            await client
                .OrganizationMemberships
                .DeleteAsync(setup.OrganizationMembership.Id);

            var getOrganisationMembershipAfterDelete = await client
                .OrganizationMemberships
                .GetAsync(setup.OrganizationMembership.Id);

            Assert.Null(getOrganisationMembershipAfterDelete);

            await Teardown(client, setup);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithInvalidId_ShouldThrow()
        {
            var client = _clientFactory.GetClient();

            await Assert.ThrowsAsync<HttpRequestException>(() => client
                .OrganizationMemberships
                .DeleteAsync(long.MaxValue));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithIdAndUserId_ShouldDeleteMembership()
        {
            var client = _clientFactory.GetClient();

            var setup = await SetupAndCreateMembership(client);

            await client
                .OrganizationMemberships
                .DeleteAsync(setup.User.Id, setup.OrganizationMembership.Id);

            var getOrganisationMembershipAfterDelete = await client
                .OrganizationMemberships
                .GetAsync(setup.OrganizationMembership.Id);

            Assert.Null(getOrganisationMembershipAfterDelete);

            await Teardown(client, setup);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithInvalidIdAndUserId_ShouldThrow()
        {
            var client = _clientFactory.GetClient();

            await Assert.ThrowsAsync<HttpRequestException>(() => client
                .OrganizationMemberships
                .DeleteAsync(long.MaxValue, long.MaxValue));
        }

        private async Task<BasicSetupResult> Setup(IZendeskClient client)
        {
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

            return new BasicSetupResult(createdOrganisation, user);
        }

        private async Task<SetupWithMembershipResult> SetupAndCreateMembership(IZendeskClient client)
        {
            var setup = await Setup(client);

            var createdOrganisationMembership = await client
                .OrganizationMemberships
                .CreateAsync(new OrganizationMembership
                {
                    OrganizationId = setup.Organization.Id,
                    UserId = setup.User.Id
                });

            var getOrganisationMembership = await client
                .OrganizationMemberships
                .GetAsync(createdOrganisationMembership.Id);

            Assert.NotNull(getOrganisationMembership);

            return new SetupWithMembershipResult(
                createdOrganisationMembership,
                setup.Organization,
                setup.User);
        }

        private async Task TeardownAndDeleteMembership(IZendeskClient client, SetupWithMembershipResult setup)
        {
            await client
                .OrganizationMemberships
                .DeleteAsync(setup.OrganizationMembership.Id);

            await Teardown(client, setup);
        }

        private async Task Teardown(IZendeskClient client, BasicSetupResult setup)
        {
            await client
                .Users
                .DeleteAsync(setup.User.Id);

            await client
                .Organizations
                .DeleteAsync(setup.Organization.Id);
        }

        internal class SetupWithMembershipResult : BasicSetupResult
        {
            public OrganizationMembership OrganizationMembership { get; }

            public SetupWithMembershipResult(
                OrganizationMembership organizationMembership,
                Organization organization, 
                UserResponse user) 
                : base(organization, user)
            {
                OrganizationMembership = organizationMembership;
            }
        }

        internal class BasicSetupResult
        {
            public Organization Organization { get; }
            public UserResponse User { get; }

            public BasicSetupResult(Organization organization, UserResponse user)
            {
                Organization = organization;
                User = user;
            }
        }
    }
}
