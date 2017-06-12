using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentsResource
    {
        Task<IEnumerable<TicketComment>> GetAllAsync(long parentId);
        Task AddComment(long ticketId, TicketComment ticketComment);
    }
}