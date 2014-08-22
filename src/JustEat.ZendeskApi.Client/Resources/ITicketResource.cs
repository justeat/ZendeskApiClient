using System.Collections.Generic;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
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
