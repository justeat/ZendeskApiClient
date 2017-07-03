using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;
using ZendeskApi.Client.Queries;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestsResource
    {
        Task<IPagination<Request>> GetAllAsync(PagerParameters pager = null);
        Task<Request> GetAsync(long requestId);
        Task<IPagination<Request>> SearchAsync(IZendeskQuery query, PagerParameters pager = null);
        Task<IPagination<TicketComment>> GetAllComments(long requestId, PagerParameters pager = null);
        Task<TicketComment> GetTicketCommentAsync(long requestId, long commentId);
        Task<Request> CreateAsync(Request request);
        Task<Request> UpdateAsync(Request request);
    }
}