using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ZendeskResource<Ticket>, ITicketResource
    {
        protected override string ResourceUri => @"/api/v2/tickets";

        public TicketResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<Ticket> Get(long id)
        {
            return GetAsync(id).Result;
        }

        public async Task<IResponse<Ticket>> GetAsync(long id)
        {
            return await GetAsync<TicketResponse>(id).ConfigureAwait(false);
        }

        public IListResponse<Ticket> GetAll(List<long> ids)
        {
            return GetAllAsync(ids).Result;
        }

        public async Task<IListResponse<Ticket>> GetAllAsync(List<long> ids)
        {
            return await GetAllAsync<TicketListResponse>(ids).ConfigureAwait(false);
        }

        public IResponse<Ticket> Put(TicketRequest request)
        {
            return PutAsync(request).Result;
        }

        public async Task<IResponse<Ticket>> PutAsync(TicketRequest request)
        {
            return await PutAsync<TicketRequest, TicketResponse>(request).ConfigureAwait(false);
        }

        public IResponse<Ticket> Post(TicketRequest request)
        {
            return PostAsync(request).Result;
        }

        public async Task<IResponse<Ticket>> PostAsync(TicketRequest request)
        {
            return await PostAsync<TicketRequest, TicketResponse>(request).ConfigureAwait(false);
        }
    }
}
