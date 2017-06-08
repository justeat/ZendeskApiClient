using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;

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
            var response = await _resource.GetAllAsync();

            var groups = response.ToArray();

            var group1 = new Group
            {
                Id = 211L,
                Name = "DJs",
                Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
            };

            var group2 = new Group
            {
                Id = 122L,
                Name = "MCs",
                Created = DateTime.Parse("2009-08-26T00:07:08Z"),
                Updated = DateTime.Parse("2010-05-13T00:07:08Z"),
            };

            Assert.Equal(2, groups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(groups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(groups[1]));
        }

        [Fact]
        public async Task ShouldListAllGroupsForUser()
        {
            var response = await _resource.GetAllAsync(123L);

            var groups = response.ToArray();

            var group1 = new Group
            {
                Id = 321L,
                Name = "Group For 123",
                Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
            };

            var group2 = new Group
            {
                Id = 342L,
                Name = "Group For 123",
                Created = DateTime.Parse("2009-08-26T00:07:08Z"),
                Updated = DateTime.Parse("2010-05-13T00:07:08Z"),
            };

            Assert.Equal(2, groups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(groups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(groups[1]));
        }

        [Fact]
        public async Task ShouldListAllAssignable()
        {
            var response = await _resource.GetAllAssignableAsync();

            var groups = response.ToArray();

            var group1 = new Group
            {
                Id = 321L,
                Name = "Group For 123",
                Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
            };

            var group2 = new Group
            {
                Id = 122L,
                Name = "MCs",
                Created = DateTime.Parse("2009-08-26T00:07:08Z"),
                Updated = DateTime.Parse("2010-05-13T00:07:08Z"),
            };

            Assert.Equal(2, groups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(groups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(groups[1]));
        }

        [Fact]
        public async Task ShouldGetDifferentGroup1ById()
        {
            var response = await _resource.GetAsync(1);

            var group = new Group
            {
                Id = 1L,
                Name = "DJs",
                Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
            };

            Assert.Equal(JsonConvert.SerializeObject(group), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task ShouldGetDifferentGroup14ById()
        {
            var response = await _resource.GetAsync(14);

            var group = new Group
            {
                Id = 14L,
                Name = "DJs",
                Created = DateTime.Parse("2009-05-13T00:07:08Z"),
                Updated = DateTime.Parse("2011-07-22T00:11:12Z"),
            };

            Assert.Equal(JsonConvert.SerializeObject(group), JsonConvert.SerializeObject(response));
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
            var response = await _resource.PutAsync(
                new Group
                {
                    Id = 213,
                    Name = "I'm an updated group!",
                    Url = new Uri("http://kung.f1u.com"),
                    HasIncidents = false
                });

            Assert.Equal(213, response.Id);
            Assert.Equal("I'm an updated group!", response.Name);
            Assert.Equal(new Uri("http://kung.f1u.com"), response.Url);
            Assert.False(response.HasIncidents);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
