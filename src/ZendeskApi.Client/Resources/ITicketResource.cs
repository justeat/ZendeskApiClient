using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketResource
    {
        Task DeleteAsync(long id);
        Task<Ticket> GetAsync(long id);
        Task<IListResponse<Ticket>> GetAllAsync(List<long> ids);
        Task<Ticket> PutAsync(TicketRequest request);
        Task<Ticket> PostAsync(TicketRequest request);
    }
}
