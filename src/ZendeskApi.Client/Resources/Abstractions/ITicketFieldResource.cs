using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldResource
    {
        Task<TicketField> GetAsync(long id);
        Task<IListResponse<TicketField>> GetAllAsync();
    }
}
