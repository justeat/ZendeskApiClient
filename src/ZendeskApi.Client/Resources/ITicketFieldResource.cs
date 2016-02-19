using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldResource
    {
        IResponse<TicketField> Get(long id);
        IListResponse<TicketField> GetAll();
    }
}
