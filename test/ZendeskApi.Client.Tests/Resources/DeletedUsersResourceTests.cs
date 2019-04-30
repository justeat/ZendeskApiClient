using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class DeletedUsersResourceTests
    {
        private readonly DeletedUsersResource _resource;

        public DeletedUsersResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<UserResponse>(resource => new DeletedUsersResourceSampleSite(resource));
            _resource = new DeletedUsersResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ListAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.ListAsync();

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal($"email.{i}", item.Email);
                Assert.Equal(i.ToString(), item.ExternalId);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithPaging_ShouldGetAll()
        {
            var results = await _resource.ListAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            });

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal("name.2", item.Name);
            Assert.Equal("2", item.ExternalId);
            Assert.Equal(2.ToString(), item.ExternalId);
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
        public async Task GetAllAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync();

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal($"email.{i}", item.Email);
                Assert.Equal(i.ToString(), item.ExternalId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithPaging_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            });

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal("name.2", item.Name);
            Assert.Equal("2", item.ExternalId);
            Assert.Equal(2.ToString(), item.ExternalId);
        }

        [Fact]
        public async Task GetAllAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGet()
        {
            var item = await _resource.GetAsync(1);

            Assert.Equal(1, item.Id);
            Assert.Equal("name.1", item.Name);
            Assert.Equal("1", item.ExternalId);
            Assert.Equal(1.ToString(), item.ExternalId);
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
        public async Task PermanentlyDeleteAsync_WhenCalled_ShouldDelete()
        {
            await _resource.PermanentlyDeleteAsync(1);
        }

        [Fact]
        public async Task PermanentlyDeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.PermanentlyDeleteAsync(int.MinValue));
        }
    }
}
