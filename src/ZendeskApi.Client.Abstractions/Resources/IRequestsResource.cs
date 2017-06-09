using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface IRequestsResource
    {
        Task<IEnumerable<TicketComment>> GetAllComments(long requestId);
        Task<TicketComment> GetTicketCommentAsync(long requestId, long commentId);
    }
}