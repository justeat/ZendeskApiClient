using System.Collections.Generic;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ZendeskResource<Ticket>, ITicketResource
    {
        protected override string ResourceUri {
            get { return @"/api/v2/tickets"; }
        }

        public TicketResource(IRestClient client)
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
