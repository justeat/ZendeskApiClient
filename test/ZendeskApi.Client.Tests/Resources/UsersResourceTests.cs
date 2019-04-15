using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
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
        public async Task ListAsync_WhenCalled_ShouldGetAllUsers()
        {
            var results = await _resource.ListAsync();

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var user = results.ElementAt(i - 1);

                Assert.Equal($"name.{i}", user.Name);
                Assert.Equal($"email.{i}", user.Email);
                Assert.Equal(i.ToString(), user.ExternalId);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithPaging_ShouldGetAllUsers()
        {
            var results = await _resource.ListAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            });

            var user = results.First();

            Assert.Equal("name.2", user.Name);
            Assert.Equal("email.2", user.Email);
            Assert.Equal("2", user.ExternalId);
        }

        [Fact]
        public async Task ListAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }));
        }

        [Fact]
        public async Task ListInGroupAsync_WhenCalled_ShouldGetAllUsers()
        {
            var results = await _resource.ListInGroupAsync(1);

            Assert.Equal(1, results.Count);

            var user = results.First();

            Assert.Equal("name.1", user.Name);
            Assert.Equal("email.1", user.Email);
            Assert.Equal("1", user.ExternalId);
            Assert.Equal(1, user.DefaultGroupId);
        }

        [Fact]
        public async Task ListInGroupAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.ListInGroupAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task ListInGroupAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListInGroupAsync(int.MinValue));
        }

        [Fact]
        public async Task ListInOrganizationAsync_WhenCalled_ShouldGetAllUsers()
        {
            var results = await _resource.ListInOrganizationAsync(1);

            Assert.Equal(1, results.Count);

            var user = results.First();

            Assert.Equal("name.1", user.Name);
            Assert.Equal("email.1", user.Email);
            Assert.Equal("1", user.ExternalId);
            Assert.Equal(1, user.DefaultGroupId);
            Assert.Equal(1, user.OrganizationId);
        }

        [Fact]
        public async Task ListInOrganizationAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.ListInOrganizationAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task ListInOrganizationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListInOrganizationAsync(int.MinValue));
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithOrganizationIds_ShouldGetAllUsers()
        {
            var results = await _resource.ListAsync(new long[] { 1, 2, 3 });

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var user = results.ElementAt(i - 1);

                Assert.Equal($"name.{i}", user.Name);
                Assert.Equal(i.ToString(), user.ExternalId);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithOrganizationIdsAndWithPaging_ShouldGetAllUsers()
        {
            var results = await _resource.ListAsync(
                new long[] { 1, 2, 3 },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var user = results.First();

            Assert.Equal("name.2", user.Name);
            Assert.Equal("2", user.ExternalId);
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithOrganizationIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAsync(new long[] { long.MinValue }));
        }

        [Fact]
        public async Task ListByExternalIdsAsync_WhenCalledWithExternalIds_ShouldGetAllUsers()
        {
            var results = await _resource.ListByExternalIdsAsync(new string[] { "1", "2", "3" });

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var user = results.ElementAt(i - 1);

                Assert.Equal($"name.{i}", user.Name);
                Assert.Equal(i.ToString(), user.ExternalId);
            }
        }

        [Fact]
        public async Task ListByExternalIdsAsync_WhenCalledWithExternalIdsAndWithPaging_ShouldGetAllUsers()
        {
            var results = await _resource.ListByExternalIdsAsync(
                new string[] { "1", "2", "3" },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var user = results.First();

            Assert.Equal("name.2", user.Name);
            Assert.Equal("2", user.ExternalId);
        }

        [Fact]
        public async Task ListByExternalIdsAsync_WhenCalledWithExternalIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListByExternalIdsAsync(new string[] { long.MinValue.ToString() }));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGetUser()
        {
            var org = await _resource.GetAsync(1);

            Assert.Equal("name.1", org.Name);
            Assert.Equal("1", org.ExternalId);
        }

        [Fact]
        public async Task GetAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(int.MinValue));
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
        public async Task CreateAsync_WhenCalled_ShouldCreateUser()
        {
            var user = await _resource.CreateAsync(
                new UserCreateRequest("name")
                {
                    Email = "Fu1@fu.com"
                });

            Assert.Equal("Fu1@fu.com", user.Email);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new UserCreateRequest("name")
            {
                Name = string.Empty
            }));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldCreateUser()
        {
            var user = await _resource.UpdateAsync(new UserUpdateRequest(1)
            {
                Id = 1,
                Name = "MyNewOrgName",
                ExternalId = "999999"
            });

            Assert.Equal("MyNewOrgName", user.Name);
            Assert.Equal("999999", user.ExternalId);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new UserUpdateRequest(int.MaxValue)
            {
                Name = "MyNewOrgName",
                ExternalId = "999999"
            });

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new UserUpdateRequest(int.MinValue)
            {
                Name = string.Empty
            }));
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
        public async Task DeleteAsync_WhenCalled_ShouldDeleteUser()
        {
            await _resource.DeleteAsync(1);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue));
        }
    }
}
