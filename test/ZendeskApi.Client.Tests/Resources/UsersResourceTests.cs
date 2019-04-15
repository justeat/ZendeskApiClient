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
    public class UsersResourceTests
    {
        private readonly UsersResource _resource;

        public UsersResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<UserResponse>(resource => new UsersResourceSampleSite(resource));
            _resource = new UsersResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var users = await CreateUsers();

            var objs = (await _resource.ListAsync()).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(users[0]), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(users[1]), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetAllUsersInGroup()
        {
            var obj1 = new UserCreateRequest("name")
            {
                Email = "Fu1@fu.com", 
                DefaultGroupId = 1
            };

            var obj2 = new UserCreateRequest("name")
            {
                Email = "Fu2@fu.com",
                DefaultGroupId = 2
            };

            var objr1 = await _resource.CreateAsync(obj1);
            var objr2 = await _resource.CreateAsync(obj2);

            var obj1Result = (await _resource.ListInGroupAsync(1)).ToArray()[0];
            var obj2Result = (await _resource.ListInGroupAsync(2)).ToArray()[0];
            
            Assert.Equal(JsonConvert.SerializeObject(objr1), JsonConvert.SerializeObject(obj1Result));
            Assert.Equal(JsonConvert.SerializeObject(objr2), JsonConvert.SerializeObject(obj2Result));
        }

        [Fact]
        public async Task ShouldGetAllUsersInOrganization()
        {
            var obj1 = new UserCreateRequest("name")
            {
                Email = "Fu1@fu.com",
                OrganizationId = 18
            };

            var obj2 = new UserCreateRequest("name")
            {
                Email = "Fu2@fu.com",
                OrganizationId = 12
            };

            var objr1 = await _resource.CreateAsync(obj1);
            var objr2 = await _resource.CreateAsync(obj2);

            var obj1Result = (await _resource.ListInOrganizationAsync(12)).ToArray()[0];
            var obj2Result = (await _resource.ListInOrganizationAsync(18)).ToArray()[0];

            Assert.Equal(JsonConvert.SerializeObject(objr1), JsonConvert.SerializeObject(obj2Result));
            Assert.Equal(JsonConvert.SerializeObject(objr2), JsonConvert.SerializeObject(obj1Result));
        }

        [Fact]
        public async Task ShouldGetAllUsersById()
        {
            var obj1 = await _resource.CreateAsync(new UserCreateRequest("name") { Email = "Fu1@fu.com" });
            var obj2 = await _resource.CreateAsync(new UserCreateRequest("name") { Email = "Fu2@fu.com" });
            var obj3 = await _resource.CreateAsync(new UserCreateRequest("name") { Email = "Fu2@fu.com" });

            var objs = (await _resource.ListAsync(new[] { obj1.Id, obj3.Id })).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj3), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetAllUsersByExternalId()
        {
            var obj1 = await _resource.CreateAsync(new UserCreateRequest("name") { Email = "Fu1@fu.com" });
            var obj2 = await _resource.CreateAsync(new UserCreateRequest("name") { Email = "Fu2@fu.com", ExternalId = "ATEST1" });
            var obj3 = await _resource.CreateAsync(new UserCreateRequest("name") { Email = "Fu2@fu.com", ExternalId = "ATEST2" });

            var objs = (await _resource.ListByExternalIdsAsync(new[] { obj1.ExternalId, obj2.ExternalId, obj3.ExternalId })).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj3), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetUser()
        {
            var user = await _resource.CreateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com"
                });

            var user2 = await _resource.GetAsync(user.Id);

            Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(user2));
        }

        [Fact]
        public async Task ShouldGetIncrementalChanges()
        {
            var user = await _resource.CreateAsync(
                new UserCreateRequest("User Name")
                {
                    Email = "test@kung.fu",
                });

            // Querying every change in the last 5 minutes
            var now = DateTime.UtcNow.Subtract(new TimeSpan(0, 5, 0));
            var changedUsers = await _resource.GetIncrementalExport(now);

            Assert.Equal(1, changedUsers.Count);
            Assert.Equal(user.Email, changedUsers.First().Email);
            Assert.False(changedUsers.HasMoreResults);

            // The second request with the end time of the previous request, should not return any
            var emptyResult = await _resource.GetIncrementalExport(changedUsers.EndTime);

            Assert.Equal(0, emptyResult.Count);
            Assert.Equal(changedUsers.EndTime, emptyResult.EndTime);
        }
        
        [Fact]
        public async Task ShouldCreateUser()
        {
            var user = await _resource.CreateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com"
                });

            Assert.Equal("Fu1@fu.com", user.Email);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            var user = await _resource.CreateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com",
                    Name = "Kung Fu Wizard"
                });

            Assert.Equal("Kung Fu Wizard", user.Name);

            var updateRequest = new UserUpdateRequest(user.Id)
            {
                Name = "Cheese Master"
            };

            var userUpdated = await _resource.UpdateAsync(updateRequest);

            Assert.Equal("Cheese Master", userUpdated.Name);
        }

        [Fact]
        public async Task ShouldCreateOrUpdateUser()
        {
            var user = await _resource.CreateOrUpdateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com",
                    Name = "Cheese Master"
                });

            Assert.Equal("Fu1@fu.com", user.Email);
            Assert.Equal("Cheese Master", user.Name);

            user = await _resource.CreateOrUpdateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com",
                    Name = "Kung Fu Wizard"
                });

            Assert.Equal("Fu1@fu.com", user.Email);
            Assert.Equal("Kung Fu Wizard", user.Name);
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            var user = await _resource.CreateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com",
                    Name = "Kung Fu Wizard"
                });

            var user1 = await _resource.GetAsync(user.Id);

            Assert.Equal(JsonConvert.SerializeObject(user), JsonConvert.SerializeObject(user1));

            await _resource.DeleteAsync(user.Id);

            var user2 = await _resource.GetAsync(user.Id);

            Assert.Null(user2);
        }

        private async Task<UserResponse[]> CreateUsers()
        {
            var obj1 = new UserCreateRequest("some name")
            {
                Email = "Fu1@fu.com"
            };

            var obj2 = new UserCreateRequest("some name")
            {
                Email = "Fu2@fu.com"
            };

            var objr1 = await _resource.CreateAsync(obj1);
            var objr2 = await _resource.CreateAsync(obj2);

            return new[] { objr1, objr2 };
        }
    }
}
