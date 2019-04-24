using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class UserFieldsResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly UserFieldsResource _resource;

        public UserFieldsResourceTests()
        {
            _client = new DisposableZendeskApiClient<UserField>((resource) => new UserFieldsResourceSampleSite(resource));
            _resource = new UserFieldsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllUserFields()
        {
            var obj1 = await _resource.CreateAsync(new UserField
            {
                RawTitle = "FuBar"
            });

            var obj2 = await _resource.CreateAsync(new UserField
            {
                RawTitle = "FuBar2"
            });

            var retrievedGroups = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, retrievedGroups.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(retrievedGroups[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(retrievedGroups[1]));
        }

        [Fact]
        public async Task ShouldGetUserFieldById()
        {
            var obj1 = await _resource.CreateAsync(new UserField
            {
                RawTitle = "FuBar"
            });

            var obj2 = await _resource.CreateAsync(new UserField
            {
                RawTitle = "FuBar2"
            });

            var obj3 = await _resource.GetAsync(obj2.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(obj3));
        }

        [Fact]
        public async Task ShouldCreateUserField()
        {
            var response = await _resource.CreateAsync(
                new UserField
                {
                    RawTitle = "FuBar"
                });

            Assert.NotNull(response.Id);
            Assert.Equal("FuBar", response.RawTitle);
        }

        [Fact]
        public async Task ShouldUpdateUserField()
        {
            var userField = await _resource.CreateAsync(
                new UserField
                {
                    RawTitle = "FuBar"
                });

            Assert.Equal("FuBar", userField.RawTitle);

            userField.RawTitle = "BarFu";

            userField = await _resource.UpdateAsync(userField);

            Assert.Equal("BarFu", userField.RawTitle);
        }

        [Fact]
        public async Task ShouldDeleteUserField()
        {
            var userField = await _resource.CreateAsync(
                new UserField
                {
                    RawTitle = "FuBar"
                });

            var userField1 = await _resource.GetAsync(userField.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(userField), JsonConvert.SerializeObject(userField1));

            await _resource.DeleteAsync(userField.Id.Value);

            var userField2 = await _resource.GetAsync(userField.Id.Value);

            Assert.Null(userField2);
        }
    }
}
