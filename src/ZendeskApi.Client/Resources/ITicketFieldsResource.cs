using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldsResource
    {
        Task<IPagination<TicketField>> GetAllAsync(PagerParameters pager = null);
        Task<TicketField> GetAsync(long ticketFieldId);
        Task<TicketField> CreateAsync(TicketField ticketField);
        Task<TicketField> UpdateAsync(TicketField ticketField);
        Task DeleteAsync(long ticketFieldId);
    }
}
