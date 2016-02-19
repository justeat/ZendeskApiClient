using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormResource
    {
        IResponse<TicketForm> Get(long id);
        IListResponse<TicketForm> GetAll();
    }
}
