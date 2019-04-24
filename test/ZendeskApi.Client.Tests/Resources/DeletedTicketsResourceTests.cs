using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class DeletedTicketsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly DeletedTicketsResource _resource;

        public DeletedTicketsResourceTests()
        {
            _client = new DisposableZendeskApiClient<TicketState, Ticket>(resource => new DeletedTicketResourceSampleSite(resource));
            _resource = new DeletedTicketsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ListAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.ListAsync();

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
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

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
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
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
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

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
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
        public async Task ListAsync_WhenCalledWithQuery_ShouldGetAll()
        {
            var results = await _resource.ListAsync(query => query.WithOrdering(SortBy.CreatedAt, SortOrder.Asc));

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithQueryWithPaging_ShouldGetAll()
        {
            var results = await _resource.ListAsync(
                query => query.WithOrdering(SortBy.CreatedAt, SortOrder.Asc),
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task ListAsync_WithQueryWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAsync(
                query => query.WithOrdering(SortBy.CreatedAt, SortOrder.Asc),
                new PagerParameters
                {
                    Page = int.MaxValue,
                    PageSize = int.MaxValue
                }));
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithQuery_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(query => query.WithOrdering(SortBy.CreatedAt, SortOrder.Asc));

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithPagingWithQuery_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(
                query => query.WithOrdering(SortBy.CreatedAt, SortOrder.Asc),
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                });

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllAsync_WithQueryWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(
                query => query.WithOrdering(SortBy.CreatedAt, SortOrder.Asc),
                new PagerParameters
                {
                    Page = int.MaxValue,
                    PageSize = int.MaxValue
                }));
        }

        [Fact]
        public async Task RestoreAsync_WhenCalled_ShouldRestoreTicket()
        {
            await _resource.RestoreAsync(1);

            Assert.Null((await _resource.GetAllAsync()).SingleOrDefault(item => item.Id == 1));
        }

        [Fact]
        public async Task RestoreAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.RestoreAsync(int.MinValue));
        }

        [Fact]
        public async Task RestoreAsync_WhenCalledWithMany_ShouldRestoreTicket()
        {
            await _resource.RestoreAsync(new long[] { 2, 3 });

            var results = await _resource.GetAllAsync();

            Assert.Null(results.SingleOrDefault(item => item.Id == 2));
            Assert.Null(results.SingleOrDefault(item => item.Id == 3));
        }

        [Fact]
        public async Task RestoreAsync_WithManyWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.RestoreAsync(new long[] { 1 }));
        }

        [Fact]
        public async Task RestoreAsync_WithNullIds_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _resource.RestoreAsync(null));
        }

        [Fact]
        public async Task RestoreAsync_WithEmptyList_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _resource.RestoreAsync(new List<long> { }));
        }

        [Fact]
        public async Task RestoreAsync_WithTooManyItemsInList_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _resource.RestoreAsync(new List<long>(101)));
        }

        [Fact]
        public async Task PurgeAsync_WhenCalled_ShouldRemoveTicket()
        {
            await _resource.PurgeAsync(10);

            Assert.Null((await _resource.GetAllAsync()).SingleOrDefault(item => item.Id == 10));
        }

        [Fact]
        public async Task PurgeAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.PurgeAsync(int.MinValue));
        }

        [Fact]
        public async Task PurgeAsync_WhenCalledWithMany_ShouldRemoveTicket()
        {
            await _resource.PurgeAsync(new long[] { 12, 13 });

            var results = await _resource.GetAllAsync();

            Assert.Null(results.SingleOrDefault(item => item.Id == 12));
            Assert.Null(results.SingleOrDefault(item => item.Id == 13));
        }

        [Fact]
        public async Task PurgeAsync_WithManyWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.PurgeAsync(new long[] { 1 }));
        }

        [Fact]
        public async Task PurgeAsync_WithNullIds_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _resource.PurgeAsync(null));
        }

        [Fact]
        public async Task PurgeAsync_WithEmptyList_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _resource.PurgeAsync(new List<long> { }));
        }

        [Fact]
        public async Task PurgeAsync_WithTooManyItemsInList_ShouldThrow()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _resource.PurgeAsync(new List<long>(101)));
        }

        public void Dispose()
        {
            ((IDisposable)_client)?.Dispose();
        }
    }
}
