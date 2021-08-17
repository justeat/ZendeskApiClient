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
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<DeletedTicketsListResponse> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<DeletedTicketsListResponse> ListAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<DeletedTicketsListResponse> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<DeletedTicketsListCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<DeletedTicketsListResponse> GetAllAsync(
            Action<IZendeskQuery> builder, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<DeletedTicketsListCursorResponse> GetAllAsync(
            Action<IZendeskQuery> builder,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task RestoreAsync(
            long ticketId,
            CancellationToken cancellationToken = default);

        Task RestoreAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> PurgeAsync(
            long ticketId,
            CancellationToken cancellationToken = default);

        Task<JobStatusResponse> PurgeAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default);
    }
}