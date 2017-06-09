using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormsResource
    {
        Task<IEnumerable<TicketForm>> GetAllAsync();
        Task<TicketForm> GetAsync(long ticketformId);
        Task<IEnumerable<TicketForm>> GetAllAsync(long[] ticketFormsIds);
        Task<TicketForm> PostAsync(TicketForm ticketForm);
        Task<TicketForm> PutAsync(TicketForm ticketForm);
        Task DeleteAsync(long ticketFormId);
    }
}
