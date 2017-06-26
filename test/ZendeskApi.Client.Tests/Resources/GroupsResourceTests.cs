using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Tests.Resources
{
    public class GroupsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly GroupsResource _resource;

        public GroupsResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new GroupsResourceSampleSite(resource));
            _resource = new GroupsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllGroups()
        {
            var group1 = await _resource.PostAsync(new Group
            {
                Name = "DJs",
                CreatedAt = DateTime.Parse("2009-05-13T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2011-07-22T00:11:12Z"),
            });

            var group2 = await _resource.PostAsync(new Group
            {
                Id = 122L,
                Name = "MCs",
                CreatedAt = DateTime.Parse("2009-08-26T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2010-05-13T00:07:08Z"),
            });

            var retrievedGroups = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, retrievedGroups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(retrievedGroups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(retrievedGroups[1]));
        }

        [Fact]
        public async Task ShouldListAllGroupsForUser()
        {
            // NOTE: Using name rather than calling user api to assign groups.
            var group1 = await _resource.PostAsync(new Group
            {
                Name = "DJs USER: 1",
                CreatedAt = DateTime.Parse("2009-05-13T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2011-07-22T00:11:12Z"),
            });

            var group2 = await _resource.PostAsync(new Group
            {
                Name = "MCs USER: 2",
                CreatedAt = DateTime.Parse("2009-08-26T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2010-05-13T00:07:08Z"),
            });

            var group3 = await _resource.PostAsync(new Group
            {
                Name = "DJs USER: 1",
                CreatedAt = DateTime.Parse("2009-08-26T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2010-05-13T00:07:08Z"),
            });

            var retrievedGroups = (await _resource.GetAllAsync(1L)).ToArray();

            Assert.Equal(2, retrievedGroups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(retrievedGroups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group3), JsonConvert.SerializeObject(retrievedGroups[1]));
        }

        [Fact]
        public async Task ShouldListAllAssignable()
        {
            var group1 = await _resource.PostAsync(new Group
            {
                Name = "DJs Assign:true",
                CreatedAt = DateTime.Parse("2009-05-13T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2011-07-22T00:11:12Z"),
            });

            var group2 = await _resource.PostAsync(new Group
            {
                Name = "MCs Assign:true",
                CreatedAt = DateTime.Parse("2009-08-26T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2010-05-13T00:07:08Z"),
            });

            var group3 = await _resource.PostAsync(new Group
            {
                Name = "DJs Assign:false",
                CreatedAt = DateTime.Parse("2009-08-26T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2010-05-13T00:07:08Z"),
            });

            var groups = (await _resource.GetAllAssignableAsync()).ToArray();

            Assert.Equal(2, groups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(groups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(groups[1]));
        }

        [Fact]
        public async Task ShouldGetGroupById()
        {
            var group1 = await _resource.PostAsync(new Group
            {
                Name = "DJs",
                CreatedAt = DateTime.Parse("2009-05-13T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2011-07-22T00:11:12Z"),
            });

            var group2 = await _resource.PostAsync(new Group
            {
                Name = "MCs",
                CreatedAt = DateTime.Parse("2009-08-26T00:07:08Z"),
                UpdatedAt = DateTime.Parse("2010-05-13T00:07:08Z"),
            });

            var group = await _resource.GetAsync(group2.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(group));
        }

        [Fact]
        public async Task ShouldCreateGroup()
        {
            var response = await _resource.PostAsync(
                new Group
                {
                    Name = "I'm a group!",
                    Url = new Uri("http://kung.fu.com"),
                    HasIncidents = true
                });

            Assert.NotNull(response.Id);
            Assert.Equal("I'm a group!", response.Name);
            Assert.Equal(new Uri("http://kung.fu.com"), response.Url);
            Assert.True(response.HasIncidents);
        }

        [Fact]
        public Task ShouldThrowErrorWhenNot201()
        {
            return Assert.ThrowsAsync<HttpRequestException>(async () => await _resource.PostAsync(
                new Group
                {
                    Name = "I'm an error group!",
                    Url = new Uri("http://kung.fu.com"),
                    HasIncidents = true
                }));

            // could use tags to simulate httpstatus codes in fake client?
        }

        [Fact]
        public async Task ShouldUpdateGroup()
        {
            var group = await _resource.PostAsync(
                new Group
                {
                    Name = "I'm a group!",
                    Url = new Uri("http://kung.fu.com"),
                    HasIncidents = true
                });

            Assert.Equal("I'm a group!", group.Name);

            group.Name = "Im a new group!";

            group = await _resource.PutAsync(group);

            Assert.Equal("Im a new group!", group.Name);
        }

        [Fact]
        public async Task ShouldDeleteGroup()
        {
            var group = await _resource.PostAsync(
                new Group
                {
                    Name = "I'm a group!",
                    Url = new Uri("http://kung.fu.com"),
                    HasIncidents = true
                });

            var group1 = await _resource.GetAsync(group.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(group), JsonConvert.SerializeObject(group1));

            await _resource.DeleteAsync(group.Id.Value);

            var group2 = await _resource.GetAsync(group.Id.Value);

            Assert.Null(group2);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
