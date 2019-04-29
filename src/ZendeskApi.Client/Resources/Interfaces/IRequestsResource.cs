using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestsResource
    {
        Task<IPagination<Request>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Request> GetAsync(
            long requestId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagination<Request>> SearchAsync(
            IZendeskQuery query, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagination<TicketComment>> GetAllComments(
            long requestId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketComment> GetTicketCommentAsync(
            long requestId, 
            long commentId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Request> CreateAsync(
            Request request,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<Request> UpdateAsync(
            Request request,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}