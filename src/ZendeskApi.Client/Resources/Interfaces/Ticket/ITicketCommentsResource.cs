using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentsResource
    {
        Task<TicketCommentsListResponse> ListAsync(
            long parentId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task AddComment(
            long ticketId, 
            TicketComment ticketComment,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}