using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentsResource
    {
        Task<IEnumerable<TicketComment>> GetAllAsync(long parentId);
        Task<TicketComment> PostAsync(TicketComment ticket);
    }
}