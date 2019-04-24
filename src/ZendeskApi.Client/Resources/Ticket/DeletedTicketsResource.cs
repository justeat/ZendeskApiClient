using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
       
        public DeletedTicketsResource(IZendeskApiClient apiClient, ILogger logger) 
            : base(apiClient, logger, "tickets")
        { }

        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<DeletedTicketsListResponse> ListAsync(PagerParameters pager = null)
        {
            return await GetAllAsync(pager);
        }
        
        public async Task<DeletedTicketsListResponse> GetAllAsync(PagerParameters pager = null)
        {
            return await GetAsync<DeletedTicketsListResponse>(
                $"{ResourceUri}",
                "show-deleted-tickets",
                "GetAllAsync",
                pager);
        }

        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<DeletedTicketsListResponse> ListAsync(Action<IZendeskQuery> builder,
            PagerParameters pager = null)
        {
            return await GetAllAsync(builder, pager);
        }
        
        public async Task<DeletedTicketsListResponse> GetAllAsync(Action<IZendeskQuery> builder, PagerParameters pager = null)
        {
            var query = new ZendeskQuery();

            builder(query);

            return await GetAsync<DeletedTicketsListResponse>(
                $"{ResourceUri}?{query.BuildQuery()}",
                "show-deleted-tickets",
                "GetAllAsync",
                pager,
                new SearchJsonConverter());
        }

        public async Task RestoreAsync(long ticketId)
        {
            await UpdateAsync(
                $"{ResourceUri}/{ticketId}/restore",
                "restore-a-previously-deleted-ticket");
        }
        
        public async Task RestoreAsync(IEnumerable<long> ticketIds)
        {
            await UpdateAsync(
                $"{ResourceUri}/restore_many",
                ticketIds.ToList(),
                "restore-previously-deleted-tickets-in-bulk");
        }

        public async Task<JobStatusResponse> PurgeAsync(long ticketId)
        {
            return await DeleteAsync<JobStatusResponse>(
                $"{ResourceUri}",
                ticketId,
                "delete-ticket-permanently",
                HttpStatusCode.OK,
                $"PurgeAsync({ticketId})");
        }

        public async Task<JobStatusResponse> PurgeAsync(IEnumerable<long> ticketIds)
        {
            var response = await DeleteAsync<SingleJobStatusResponse>(
                $"{ResourceUri}/destroy_many",
                ticketIds.ToList(),
                "delete-multiple-tickets-permanently");

            return response?
                .JobStatus;
        }
    }
}
