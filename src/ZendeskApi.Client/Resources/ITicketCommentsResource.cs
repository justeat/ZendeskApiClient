using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketCommentsResource
    {
        Task<IPagination<TicketComment>> GetAllAsync(long parentId, PagerParameters pager = null);
        Task AddComment(long ticketId, TicketComment ticketComment);
    }
}