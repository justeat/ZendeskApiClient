using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public class DeletedTicketsResource : AbstractBaseResource<DeletedTicketsResource>, 
        IDeletedTicketsResource
    {
        private const string ResourceUri = "api/v2/deleted_tickets";
       
        public DeletedTicketsResource(
            IZendeskApiClient apiClient, 
            ILogger logger) 
            : base(apiClient, logger, "tickets")
        { }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<DeletedTicketsListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                pager, 
                cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<DeletedTicketsListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<DeletedTicketsListResponse>(
                $"{ResourceUri}",
                "show-deleted-tickets",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<DeletedTicketsListCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync<DeletedTicketsListCursorResponse>(
                $"{ResourceUri}",
                "show-deleted-tickets",
                "GetAllAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<DeletedTicketsListResponse> ListAsync(
            Action<IZendeskQuery> builder,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            return await GetAllAsync(
                builder, 
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        public async Task<DeletedTicketsListResponse> GetAllAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
        {
            var query = new ZendeskQuery();

            builder(query);

            return await GetAsync<DeletedTicketsListResponse>(
                $"{ResourceUri}?{query.BuildQuery()}",
                "show-deleted-tickets",
                "GetAllAsync",
                pager,
                new SearchJsonConverter(),
                cancellationToken);
        }

        public async Task<DeletedTicketsListCursorResponse> GetAllAsync(
            Action<IZendeskQuery> builder,
            CursorPager pager,
            CancellationToken cancellationToken = default)
        {
            var query = new ZendeskQuery();

            builder(query);

            return await GetAsync<DeletedTicketsListCursorResponse>(
                $"{ResourceUri}?{query.BuildQuery()}",
                "show-deleted-tickets",
                "GetAllAsync",
                pager,
                new SearchJsonCursorConverter(),
                cancellationToken);
        }

        public async Task RestoreAsync(
            long ticketId,
            CancellationToken cancellationToken = default)
        {
            await UpdateAsync(
                $"{ResourceUri}/{ticketId}/restore",
                "restore-a-previously-deleted-ticket",
                cancellationToken: cancellationToken);
        }
        
        public async Task RestoreAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default)
        {
            await UpdateAsync(
                $"{ResourceUri}/restore_many",
                ticketIds.ToList(),
                "restore-previously-deleted-tickets-in-bulk",
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> PurgeAsync(
            long ticketId,
            CancellationToken cancellationToken = default)
        {
            return await DeleteAsync<JobStatusResponse>(
                $"{ResourceUri}",
                ticketId,
                "delete-ticket-permanently",
                HttpStatusCode.OK,
                $"PurgeAsync({ticketId})",
                cancellationToken);
        }

        public async Task<JobStatusResponse> PurgeAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default)
        {
            var response = await DeleteAsync<SingleJobStatusResponse>(
                $"{ResourceUri}/destroy_many",
                ticketIds.ToList(),
                "delete-multiple-tickets-permanently",
                cancellationToken: cancellationToken);

            return response?
                .JobStatus;
        }
    }
}
