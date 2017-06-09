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
        Task<User> GetRelatedUsersAsync(long userId);
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
            var obj1 = new User
            {
                Email = "Fu1@fu.com", 
                DefaultGroupId = 1
            };

            var obj2 = new User
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
            var obj1 = new User
            {
                Email = "Fu1@fu.com",
                OrganizationId = 18
            };

            var obj2 = new User
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
        public async Task ShouldGetAllUsersById()
        {
            var obj1 = await _resource.PostAsync(new User { Email = "Fu1@fu.com" });
            var obj2 = await _resource.PostAsync(new User { Email = "Fu2@fu.com" });
            var obj3 = await _resource.PostAsync(new User { Email = "Fu2@fu.com" });

            var objs = (await _resource.GetAllAsync(new[] { obj1.Id.Value, obj3.Id.Value })).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj3), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetAllUsersByExternalId()
        {
            var obj1 = await _resource.PostAsync(new User { Email = "Fu1@fu.com" });
            var obj2 = await _resource.PostAsync(new User { Email = "Fu2@fu.com", ExternalId = "ATEST1" });
            var obj3 = await _resource.PostAsync(new User { Email = "Fu2@fu.com", ExternalId = "ATEST2" });

            var objs = (await _resource.GetAllByExternalIdsAsync(new[] { obj1.ExternalId, obj2.ExternalId, obj3.ExternalId })).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj3), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            var user = await _resource.PostAsync(
                new User
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
                new User
                {
                    Email = "Fu1@fu.com"
                });

            Assert.NotNull(user.Id);
            Assert.Equal("Fu1@fu.com", user.Email);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            var user = await _resource.PostAsync(
                new User
                {
                    Email = "Fu1@fu.com",
                    Name = "Kung Fu Wizard"
                });

            Assert.Equal("Kung Fu Wizard", user.Name);

            user.Name = "Cheese Master";

            user = await _resource.PutAsync(user);

            Assert.Equal("Cheese Master", user.Name);
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            var user = await _resource.PostAsync(
                new User
                {
                    Email = "Fu1@fu.com",
                    Name = "Kung Fu Wizard"
                });

            var user1 = await _resource.GetAsync(user.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(user1));

            await _resource.DeleteAsync(user.Id.Value);

            var user2 = await _resource.GetAsync(user.Id.Value);

            Assert.Null(user2);
        }

        private async Task<User[]> CreateUsers()
        {
            var obj1 = new User
            {
                Email = "Fu1@fu.com"
            };

            var obj2 = new User
            {
                Email = "Fu2@fu.com"
            };

            obj1 = await _resource.PostAsync(obj1);
            obj2 = await _resource.PostAsync(obj2);

            return new[] { obj1, obj2 };
        }
    }
}
