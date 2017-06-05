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
        Task<IResponse<Ticket>> GetAsync(long id);
        Task<IListResponse<Ticket>> GetAllAsync(List<long> ids);
        Task<IResponse<Ticket>> PutAsync(TicketRequest request);
        Task<IResponse<Ticket>> PostAsync(TicketRequest request);
    }
}
