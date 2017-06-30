using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormsResource
    {
        Task<IPagination<TicketForm>> GetAllAsync(PagerParameters pager = null);
        Task<TicketForm> GetAsync(long ticketformId);
        Task<IPagination<TicketForm>> GetAllAsync(long[] ticketFormsIds, PagerParameters pager = null);
        Task<TicketForm> CreateAsync(TicketForm ticketForm);
        Task<TicketForm> UpdateAsync(TicketForm ticketForm);
        Task DeleteAsync(long ticketFormId);
    }
}
