using System.Collections.Generic;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketResource
    {
        IResponse<Ticket> Get(long id);
        IListResponse<Ticket> GetAll(List<long> ids);
        IResponse<Ticket> Put(TicketRequest request);
        IResponse<Ticket> Post(TicketRequest request);
        void Delete(long id);
    }
}
