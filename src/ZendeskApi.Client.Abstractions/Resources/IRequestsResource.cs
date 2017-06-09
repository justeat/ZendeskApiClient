using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Queries;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestsResource
    {
        Task<IEnumerable<Request>> GetAllAsync();
        Task<Request> GetAsync(long requestId);
        Task<IEnumerable<Request>> SearchAsync(IZendeskQuery<Request> query);
        Task<IEnumerable<TicketComment>> GetAllComments(long requestId);
        Task<TicketComment> GetTicketCommentAsync(long requestId, long commentId);
        Task<Request> PostAsync(Request request);
        Task<Request> PutAsync(Request request);
    }
}