using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldResource
    {
        IResponse<TicketField> Get(long id);
        Task<IResponse<TicketField>> GetAsync(long id);
        IListResponse<TicketField> GetAll();
        Task<IListResponse<TicketField>> GetAllAsync();
    }
}
