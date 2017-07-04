using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class SearchResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly SearchResource _resource;

        public SearchResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new SearchResourceSampleSite(resource));
            _resource = new SearchResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ShouldGetObjectsWhenAsked() {
            var results = await _resource.SearchAsync(query => { });

            Assert.Equal(4, results.Count);
            Assert.Equal(1, results.OfType<TicketResponse>().Single().Id);
            Assert.Equal(3, results.OfType<Organization>().Single().Id);
           // Assert.Equal(2, results.OfType<Group>().Single().Id);
            Assert.Equal(4, results.OfType<UserResponse>().Single().Id);
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var results = await _resource.SearchAsync<TicketResponse>(query => { });

            Assert.Equal(1, results.Count);
            Assert.Equal(1, results.Single().Id);
        }
    }
}
