using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
using Group = ZendeskApi.Client.Models.Group;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class SearchResourceTests
    {
        private readonly SearchResource _resource;

        public SearchResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<ISearchResult>(resource => new SearchResourceSampleSite(resource));
            _resource = new SearchResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task SearchAsync_WhenCalled_ShouldGetObjects() {
            var results = await _resource.SearchAsync(query => { });

            Assert.Equal(5, results.Count);
            Assert.Equal(1, results.OfType<Ticket>().First().Id);
            Assert.Equal(2, results.OfType<Group>().Single().Id);
            Assert.Equal(3, results.OfType<Organization>().Single().Id);
            Assert.Equal(4, results.OfType<UserResponse>().Single().Id);
        }

        [Fact]
        public async Task SearchAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.SearchAsync(
                query => { }, 
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal(new Uri("https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
        }

        [Fact]
        public async Task SearchAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.SearchAsync(
                query => { },
                new PagerParameters
                {
                    Page = int.MaxValue,
                    PageSize = int.MaxValue
                }));
        }

        [Fact]
        public async Task SearchAsync_WhenCalledWithType_ShouldGetTicket()
        {
            var results = await _resource.SearchAsync<Ticket>(query => { });

            Assert.Equal(2, results.Count);
            Assert.Equal(1, results.First().Id);
        }

        [Fact]
        public async Task SearchAsync_WhenCalledWithTypeAndWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.SearchAsync<Ticket>(
                query => { },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var item = results.First();

            Assert.Equal(5, item.Id);
            Assert.Equal(new Uri("https://company.zendesk.com/api/v2/tickets/5.json"), item.Url);
        }

        [Fact]
        public async Task SearchAsync_WhenCalledWithTypeWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.SearchAsync<Ticket>(
                query => { },
                new PagerParameters
                {
                    Page = int.MaxValue,
                    PageSize = int.MaxValue
                }));
        }
    }
}
