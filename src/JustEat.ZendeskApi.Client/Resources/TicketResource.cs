using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class TicketResource : ZendeskResource<Ticket>, ITicketResource
    {
        protected override string ResourceUri {
            get { return @"/api/v2/tickets.json"; }
        }

        public TicketResource(IBaseClient client)
        {
            Client = client;
        }

        public IResponse<Ticket> Get(long id)
        {
            return Get<TicketResponse>(id);
        }

        public IListResponse<Ticket> GetAll(List<long> ids)
        {
            return GetAll<TicketListResponse>(ids);
        }

        public IResponse<Ticket> Put(TicketRequest request)
        {
            return Put<TicketRequest, TicketResponse>(request);
        }

        public IResponse<Ticket> Post(TicketRequest request)
        {
            return Post<TicketRequest, TicketResponse>(request);
        }
    }
}
