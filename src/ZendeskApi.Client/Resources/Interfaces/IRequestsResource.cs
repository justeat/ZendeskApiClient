using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<Request>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<Request> GetAsync(
            long requestId,
            CancellationToken cancellationToken = default);

        Task<IPagination<Request>> SearchAsync(
            IZendeskQuery query, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllComments` with CursorPager parameter instead.")]
        Task<IPagination<TicketComment>> GetAllComments(
            long requestId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<TicketComment> GetTicketCommentAsync(
            long requestId, 
            long commentId,
            CancellationToken cancellationToken = default);

        Task<Request> CreateAsync(
            Request request,
            CancellationToken cancellationToken = default);

        Task<Request> UpdateAsync(
            Request request,
            CancellationToken cancellationToken = default);
    }
}