using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestResource : IRequestResource
    {
        private readonly IRestClient _client;
        private const string ResourceUri = "/api/v2/requests";

        public RequestResource(IRestClient client)
        {
            _client = client;
        }

        public IResponse<Request> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<RequestResponse>(requestUri);
        }

        public async Task<IResponse<Request>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<RequestResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<Request> Get(IEnumerable<TicketStatus> requestedStatuses)
        {
            string query = $"status={string.Join(",", requestedStatuses).ToLower()}";

            var requestUri = _client.BuildUri(ResourceUri, query);
            return _client.Get<RequestResponse>(requestUri);
        }

        public async Task<IResponse<Request>> GetAsync(IEnumerable<TicketStatus> requestedStatuses)
        {
            string query = $"status={string.Join(",", requestedStatuses).ToLower()}";

            var requestUri = _client.BuildUri(ResourceUri, query);
            return await _client.GetAsync<RequestResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<Request> Put(RequestRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return _client.Put<RequestResponse>(requestUri, request);
        }

        public async Task<IResponse<Request>> PutAsync(RequestRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return await _client.PutAsync<RequestResponse>(requestUri, request).ConfigureAwait(false);
        }

        public IResponse<Request> Post(RequestRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return _client.Post<RequestResponse>(requestUri, request);
        }

        public async Task<IResponse<Request>> PostAsync(RequestRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return await _client.PostAsync<RequestResponse>(requestUri, request).ConfigureAwait(false);
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
    }
}
