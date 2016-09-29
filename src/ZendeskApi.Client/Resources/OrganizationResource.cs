using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class OrganizationResource : IOrganizationResource
    {
        private readonly IRestClient _client;
        private const string ResourceUri = "/api/v2/organizations";

        public OrganizationResource(IRestClient client)
        {
            _client = client;
        }

        public IResponse<Organization> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<OrganizationResponse>(requestUri);
        }

        public async Task<IResponse<Organization>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<OrganizationResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<Organization> Put(OrganizationRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return _client.Put<OrganizationResponse>(requestUri, request);
        }

        public async Task<IResponse<Organization>> PutAsync(OrganizationRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return await _client.PutAsync<OrganizationResponse>(requestUri, request).ConfigureAwait(false);
        }

        public IResponse<Organization> Post(OrganizationRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return _client.Post<OrganizationResponse>(requestUri, request);
        }

        public async Task<IResponse<Organization>> PostAsync(OrganizationRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return await _client.PostAsync<OrganizationResponse>(requestUri, request).ConfigureAwait(false);
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            await _client.DeleteAsync(requestUri).ConfigureAwait(false);
        }

        public void Delete(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            _client.Delete(requestUri);
        }
    }
}
