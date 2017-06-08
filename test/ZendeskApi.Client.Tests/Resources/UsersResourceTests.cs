using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Tests.Resources
{
    public class UsersResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly UsersResource _resource;

        public UsersResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new UsersResourceSampleSite(resource));
            _resource = new UsersResource(_client, NullLogger.Instance);
        }

        /*
        Task<IEnumerable<User>> GetAllAsync(long[] userIds);
        Task<IEnumerable<User>> GetAllByExternalIdsAsync(long[] externalIds);
        Task<User> GetRelatedUsersAsync(long userId);
        Task<User> PutAsync(UserRequest request);
        Task DeleteAsync(long userId);
         */

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var users = await CreateUsers();

            var objs = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(users[0]), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(users[1]), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetAllUsersInGroup()
        {
            var obj1 = new Contracts.Models.User
            {
                Email = "Fu1@fu.com", 
                DefaultGroupId = 1
            };

            var obj2 = new Contracts.Models.User
            {
                Email = "Fu2@fu.com",
                DefaultGroupId = 2
            };

            obj1 = await _resource.PostAsync(obj1);
            obj2 = await _resource.PostAsync(obj2);

            var obj1Result = (await _resource.GetAllUsersInGroupAsync(1)).ToArray()[0];
            var obj2Result = (await _resource.GetAllUsersInGroupAsync(2)).ToArray()[0];
            
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(obj1Result));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(obj2Result));
        }

        [Fact]
        public async Task ShouldGetAllUsersInOrganization()
        {
            var obj1 = new Contracts.Models.User
            {
                Email = "Fu1@fu.com",
                OrganizationId = 18
            };

            var obj2 = new Contracts.Models.User
            {
                Email = "Fu2@fu.com",
                OrganizationId = 12
            };

            obj1 = await _resource.PostAsync(obj1);
            obj2 = await _resource.PostAsync(obj2);

            var obj1Result = (await _resource.GetAllUsersInOrganizationAsync(12)).ToArray()[0];
            var obj2Result = (await _resource.GetAllUsersInOrganizationAsync(18)).ToArray()[0];

            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(obj2Result));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(obj1Result));
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            var user = await _resource.PostAsync(
                new Contracts.Models.User
                {
                    Email = "Fu1@fu.com"
                });

            var user2 = await _resource.GetAsync(user.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(user2));
        }
        
        [Fact]
        public async Task ShouldCreateUser()
        {
            var user = await _resource.PostAsync(
                new Contracts.Models.User
                {
                    Email = "Fu1@fu.com"
                });

            Assert.NotNull(user.Id);
            Assert.Equal("Fu1@fu.com", user.Email);
        }

        private async Task<User[]> CreateUsers()
        {
            var obj1 = new Contracts.Models.User
            {
                Email = "Fu1@fu.com"
            };

            var obj2 = new Contracts.Models.User
            {
                Email = "Fu2@fu.com"
            };

            obj1 = await _resource.PostAsync(obj1);
            obj2 = await _resource.PostAsync(obj2);

            return new[] { obj1, obj2 };
        }
    }
}
