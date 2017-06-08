using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormResource
    {
        Task<TicketForm> GetAsync(long id);
        Task<IListResponse<TicketForm>> GetAllAsync();
    }
}
