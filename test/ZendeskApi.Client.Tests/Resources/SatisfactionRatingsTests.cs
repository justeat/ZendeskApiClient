using System;
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
    public class SatisfactionRatingsTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly SatisfactionRatingsResource _resource;

        public SatisfactionRatingsTests()
        {
            _client = new DisposableZendeskApiClient(resource => new SatisfactionRatingsResourceSampleSite(resource));
            _resource = new SatisfactionRatingsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldListAllSatisfactionRatings()
        {
            var obj1 = await _resource.CreateAsync(new SatisfactionRating { Url = "http://fu.com" }, 1);
            var obj2 = await _resource.CreateAsync(new SatisfactionRating { Url = "http://fu2.com" }, 1);

            var objs = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[1]));
        }
        
        [Fact]
        public async Task ShouldGetSatisfactionRating()
        {
            var obj1 = await _resource.CreateAsync(new SatisfactionRating { Url = "http://fu.com" }, 1);

            var response = await _resource.GetAsync(obj1.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task ShouldHandleSatisfactionRatingComment()
        {
            var obj1 = await _resource.CreateAsync(new SatisfactionRating
            {
                Url = "http://fu.com",
                Comment = "Very satified indeed"
            }, 1);

            var response = await _resource.GetAsync(obj1.Id.Value);

            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(response));
        }
        
        [Fact]
        public async Task ShouldCreateTicket()
        {
            var obj1 = await _resource.CreateAsync(new SatisfactionRating { Url = "http://fu.com" }, 1);

            Assert.NotNull(obj1.Id);
            Assert.Equal("http://fu.com", obj1.Url);
        }
        
        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
