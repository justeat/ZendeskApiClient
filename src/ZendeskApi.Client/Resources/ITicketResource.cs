using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketResource
    {
        void Delete(long id);
        Task DeleteAsync(long id);
        IResponse<Ticket> Get(long id);
        Task<IResponse<Ticket>> GetAsync(long id);
        IListResponse<Ticket> GetAll(List<long> ids);
        Task<IListResponse<Ticket>> GetAllAsync(List<long> ids);
        IResponse<Ticket> Put(TicketRequest request);
        Task<IResponse<Ticket>> PutAsync(TicketRequest request);
        IResponse<Ticket> Post(TicketRequest request);
        Task<IResponse<Ticket>> PostAsync(TicketRequest request);
    }
}
