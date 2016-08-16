using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormResource
    {
        IResponse<TicketForm> Get(long id);
        Task<IResponse<TicketForm>> GetAsync(long id);
        IListResponse<TicketForm> GetAll();
        Task<IListResponse<TicketForm>> GetAllAsync();
    }
}
