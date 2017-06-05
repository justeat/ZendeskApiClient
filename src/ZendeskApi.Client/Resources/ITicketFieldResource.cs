using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldResource
    {
        Task<IResponse<TicketField>> GetAsync(long id);
        Task<IListResponse<TicketField>> GetAllAsync();
    }
}
