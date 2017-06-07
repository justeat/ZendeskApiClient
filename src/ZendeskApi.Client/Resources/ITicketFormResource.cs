using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormResource
    {
        Task<TicketForm> GetAsync(long id);
        Task<IListResponse<TicketForm>> GetAllAsync();
    }
}
