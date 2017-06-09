using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentResource
    {
        Task<IEnumerable<TicketComment>> GetAllAsync(long parentId);
        Task<TicketComment> PostAsync(TicketComment ticket);
    }
}