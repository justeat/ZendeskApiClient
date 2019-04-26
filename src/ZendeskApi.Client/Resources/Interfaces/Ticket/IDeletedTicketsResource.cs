using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IDeletedTicketsResource
    {
        [Obsolete("Use `GetAllAsync` instead.")]
        Task<DeletedTicketsListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use `GetAllAsync` instead.")]
        Task<DeletedTicketsListResponse> ListAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<DeletedTicketsListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<DeletedTicketsListResponse> GetAllAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task RestoreAsync(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task RestoreAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<JobStatusResponse> PurgeAsync(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<JobStatusResponse> PurgeAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}