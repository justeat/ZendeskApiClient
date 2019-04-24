using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class DeletedUsersResourceTests
    {
        private readonly DeletedUsersResource _resource;
        private DeletedUsersResourceSampleSite _sample;

        public DeletedUsersResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<UserResponse>(resource =>
            {
                _sample = new DeletedUsersResourceSampleSite(resource);
                return _sample;
            });
            _resource = new DeletedUsersResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldGetAllDeletedUsers()
        {
            var objs = (await _resource.ListAsync()).ToArray();

            Assert.Equal(2, objs.Length);
        }

        [Fact]
        public async Task ShouldGetAllUsersById()
        {
            var user1 = await _resource.GetAsync(1);

            Assert.NotNull(user1);

            var user2 = await _resource.GetAsync(2);

            Assert.NotNull(user2);
        }

        [Fact]
        public async Task ShouldPermanentlyDeleteUser()
        {
            var user = new UserResponse()
            {
                Id = 1,
                Email = "Fu1@fu.com",
                Name = "Kung Fu Wizard"
            };

            await _resource.PermanentlyDeleteAsync(user.Id);

            var user2 = await _resource.GetAsync(user.Id);

            Assert.Null(user2);
        }
    }
}
