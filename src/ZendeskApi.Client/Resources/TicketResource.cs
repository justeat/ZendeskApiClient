using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ZendeskResource<Ticket>, ITicketResource
    {
        private const string ResourceUri = "/api/v2/tickets";

        public TicketResource(IRestClient client)
        {
            Client = client;
        }

        public void Delete(long id)
        {
            ValidateRequest(id);
            Delete($"{ResourceUri}/{id}");
        }

        public async Task DeleteAsync(long id)
        {
            ValidateRequest(id);
            await DeleteAsync($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public IResponse<Ticket> Get(long id)
        {
            return Get<TicketResponse>($"{ResourceUri}/{id}");
        }

        public async Task<IResponse<Ticket>> GetAsync(long id)
        {
            return await GetAsync<TicketResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public IListResponse<Ticket> GetAll(List<long> ids)
        {
            return GetAll<TicketListResponse>($"{ResourceUri}/show_many", ids);
        }

        public async Task<IListResponse<Ticket>> GetAllAsync(List<long> ids)
        {
            return await GetAllAsync<TicketListResponse>($"{ResourceUri}/show_many", ids).ConfigureAwait(false);
        }

        public IResponse<Ticket> Put(TicketRequest request)
        {
            ValidateRequest(request);
            return Put<TicketRequest, TicketResponse>(request, $"{ResourceUri}/{request.Item.Id}");
        }

        public async Task<IResponse<Ticket>> PutAsync(TicketRequest request)
        {
            ValidateRequest(request);
            return await PutAsync<TicketRequest, TicketResponse>(request, $"{ResourceUri}/{request.Item.Id}").ConfigureAwait(false);
        }

        public IResponse<Ticket> Post(TicketRequest request)
        {
            return Post<TicketRequest, TicketResponse>(request, ResourceUri);
        }

        public async Task<IResponse<Ticket>> PostAsync(TicketRequest request)
        {
            return await PostAsync<TicketRequest, TicketResponse>(request, ResourceUri).ConfigureAwait(false);
        }
    }
}
