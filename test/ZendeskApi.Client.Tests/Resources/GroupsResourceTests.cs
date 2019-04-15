using System;
using System.Linq;
using System.Net.Http;
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
    public class GroupsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly GroupsResource _resource;

        public GroupsResourceTests()
        {
            _client = new DisposableZendeskApiClient<Group>((resource) => new GroupsResourceSampleSite(resource));
            _resource = new GroupsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllGroups()
        {
            var group1 = await _resource.CreateAsync(new GroupCreateRequest("DJs"));

            var group2 = await _resource.CreateAsync(new GroupCreateRequest("MCs"));

            var retrievedGroups = (await _resource.ListAsync()).ToArray();

            Assert.Equal(2, retrievedGroups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(retrievedGroups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(retrievedGroups[1]));
        }

        [Fact]
        public async Task ShouldListAllGroupsForUser()
        {
            // NOTE: Using name rather than calling user api to assign groups.
            var group1 = await _resource.CreateAsync(new GroupCreateRequest("DJs USER: 1"));

            var group2 = await _resource.CreateAsync(new GroupCreateRequest("MCs USER: 2"));

            var group3 = await _resource.CreateAsync(new GroupCreateRequest("DJs USER: 1"));

            var retrievedGroups = (await _resource.ListAsync(1L)).ToArray();

            Assert.Equal(2, retrievedGroups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(retrievedGroups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group3), JsonConvert.SerializeObject(retrievedGroups[1]));
        }

        [Fact]
        public async Task ShouldListAllAssignable()
        {
            var group1 = await _resource.CreateAsync(new GroupCreateRequest("DJs Assign:true"));

            var group2 = await _resource.CreateAsync(new GroupCreateRequest("MCs Assign:true"));

            var group3 = await _resource.CreateAsync(new GroupCreateRequest("DJs Assign:false"));

            var groups = (await _resource.ListAssignableAsync()).ToArray();

            Assert.Equal(2, groups.Length);
            Assert.Equal(JsonConvert.SerializeObject(group1), JsonConvert.SerializeObject(groups[0]));
            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(groups[1]));
        }

        [Fact]
        public async Task ShouldGetGroupById()
        {
            var group1 = await _resource.CreateAsync(new GroupCreateRequest("DJs"));

            var group2 = await _resource.CreateAsync(new GroupCreateRequest("MCs"));

            var group = await _resource.GetAsync(group2.Id);

            Assert.Equal(JsonConvert.SerializeObject(group2), JsonConvert.SerializeObject(group));
        }

        [Fact]
        public async Task ShouldCreateGroup()
        {
            var response = await _resource.CreateAsync(new GroupCreateRequest("I'm a group!"));
            
            Assert.Equal("I'm a group!", response.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{response.Id}.json"), response.Url);
        }

        [Fact]
        public Task ShouldThrowErrorWhenNot201OnCreate()
        {
            return Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new GroupCreateRequest("I'm an error group!")));

            // could use tags to simulate httpstatus codes in fake client?
        }
        
        

        [Fact]
        public async Task ShouldUpdateGroup()
        {
            var group = await _resource.CreateAsync(new GroupCreateRequest("I'm a group!"));

            Assert.Equal("I'm a group!", group.Name);

            var groupUpdate = await _resource.UpdateAsync(new GroupUpdateRequest(group.Id)
            {
                Name = "Im a new group!"
            });

            Assert.Equal("Im a new group!", groupUpdate.Name);
        }

        [Fact]
        public async Task ShouldDeleteGroup()
        {
            var group = await _resource.CreateAsync(new GroupCreateRequest ("I'm a group!"));

            var group1 = await _resource.GetAsync(group.Id);

            Assert.Equal(JsonConvert.SerializeObject(group), JsonConvert.SerializeObject(group1));

            await _resource.DeleteAsync(group.Id);

            var group2 = await _resource.GetAsync(group.Id);

            Assert.Null(group2);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
