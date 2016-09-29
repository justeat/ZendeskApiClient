using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ITicketResource
    {
        private readonly IRestClient _client;
        private const string ResourceUri = "/api/v2/tickets";

        public TicketResource(IRestClient client)
        {
            _client = client;
        }

        public void Delete(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            _client.Delete(requestUri);
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            await _client.DeleteAsync(requestUri).ConfigureAwait(false);
        }

        public IResponse<Ticket> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<TicketResponse>(requestUri);
        }

        public async Task<IResponse<Ticket>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<TicketResponse>(requestUri).ConfigureAwait(false);
        }

        public IListResponse<Ticket> GetAll(List<long> ids)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/show_many", $"ids={ZendeskFormatter.ToCsv(ids)}");
            return _client.Get<TicketListResponse>(requestUri);
        }

        public async Task<IListResponse<Ticket>> GetAllAsync(List<long> ids)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/show_many", $"ids={ZendeskFormatter.ToCsv(ids)}");
            return await _client.GetAsync<TicketListResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<Ticket> Put(TicketRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return _client.Put<TicketResponse>(requestUri, request);
        }

        public async Task<IResponse<Ticket>> PutAsync(TicketRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return await _client.PutAsync<TicketResponse>(requestUri, request);
        }

        public IResponse<Ticket> Post(TicketRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return _client.Post<TicketResponse>(requestUri, request);
        }

        public async Task<IResponse<Ticket>> PostAsync(TicketRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return await _client.PostAsync<TicketResponse>(requestUri, request).ConfigureAwait(false);
        }
    }
}
