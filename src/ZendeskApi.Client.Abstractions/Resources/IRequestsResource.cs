using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestsResource
    {
        Task<IPagination<Request>> GetAllAsync();
        Task<Request> GetAsync(long requestId);
        Task<IPagination<Request>> SearchAsync(IZendeskQuery<Request> query);
        Task<IPagination<TicketComment>> GetAllComments(long requestId);
        Task<TicketComment> GetTicketCommentAsync(long requestId, long commentId);
        Task<Request> PostAsync(Request request);
        Task<Request> PutAsync(Request request);
    }
}