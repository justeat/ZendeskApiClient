using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class UserIdentitiesResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly UserIdentitiesResource _resource;

        public UserIdentitiesResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new UserIdentitiesResourceSampleSite(resource));
            _resource = new UserIdentitiesResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllIdentitiesForUser()
        {
            var obj1 = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 123,
                Value = "fu"
            }, 123);

            var obj2 = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 234123,
                Value = "fasdsadsau"
            }, 234123);

            var objs1 = (await _resource.GetAllForUserAsync(123)).ToArray();
            var objs2 = (await _resource.GetAllForUserAsync(234123)).ToArray();

            Assert.Single(objs1);
            Assert.Single(objs2);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs1[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs2[0]));
        }

        [Fact]
        public async Task ShouldGetIdentityForUser()
        {
            var obj1 = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 123,
                Value = "fu"
            }, 123);

            var obj2 = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 234123,
                Value = "fasdsadsau"
            }, 234123);

            var objs1 = await _resource.GetIdentityForUserAsync(123, obj1.Id.Value);
            var objs2 = await _resource.GetIdentityForUserAsync(234123, obj2.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs1));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs2));
        }
        
        [Fact]
        public async Task ShouldCreateUserIdentity()
        {
            var obj1 = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 123,
                Value = "fu"
            }, 123);

            Assert.NotNull(obj1.Id);
            Assert.Equal("fu", obj1.Value);
        }

        [Fact]
        public Task ShouldThrowErrorWhenNot201()
        {
            return Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateUserIdentityAsync(
                new UserIdentity
                {
                    UserId = 123,
                    Value = "fu error"
                }, 123));

            // could use tags to simulate httpstatus codes in fake client?
        }

        [Fact]
        public async Task ShouldUpdateGroup()
        {
            var obj = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 123,
                Value = "fu"
            }, 123);

            Assert.Equal("fu", obj.Value);

            obj.Value = "kung fu!";

            obj = await _resource.UpdateAsync(obj);

            Assert.Equal("kung fu!", obj.Value);
        }

        [Fact]
        public async Task ShouldDeleteGroup()
        {
            var obj = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                UserId = 123,
                Value = "fu"
            }, 123);

            var obj1 = await _resource.GetIdentityForUserAsync(obj.UserId.Value, obj.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj), JsonConvert.SerializeObject(obj1));

            await _resource.DeleteAsync(obj.UserId.Value, obj.Id.Value);

            var obj2 = await _resource.GetIdentityForUserAsync(obj.UserId.Value, obj.Id.Value);

            Assert.Null(obj2);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}