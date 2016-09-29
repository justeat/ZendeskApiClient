using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentityResource : IUserIdentityResource
    {
        private readonly IRestClient _client;

        public UserIdentityResource(IRestClient client)
        {
            _client = client;
        }

        public IListResponse<UserIdentity> GetAll(long id)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{id}/identities");
            return _client.Get<UserIdentityListResponse>(requestUri);
        }

        public async Task<IListResponse<UserIdentity>> GetAllAsync(long id)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{id}/identities");
            return await _client.GetAsync<UserIdentityListResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<UserIdentity> Post(UserIdentityRequest request)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{request.Item.UserId}/identities");
            return _client.Post<UserIdentityResponse>(requestUri, request);
        }

        public async Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{request.Item.UserId}/identities");
            return await _client.PostAsync<UserIdentityResponse>(requestUri, request).ConfigureAwait(false);
        }

        public IResponse<UserIdentity> Put(UserIdentityRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"/api/v2/users/{request.Item.UserId}/identities");
            return _client.Put<UserIdentityResponse>(requestUri, request);
        }

        public async Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request)
        {
            var requestUri = _client.BuildUri($"/api/v2/users/{request.Item.UserId}/identities");
            return await _client.PutAsync<UserIdentityResponse>(requestUri, request).ConfigureAwait(false);
        }

        public void Delete(long id, long parentId)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"/api/v2/users/{parentId}/identities/{id}");
            _client.Delete(requestUri);
        }

        public async Task DeleteAsync(long id, long parentId)
        {
            if (id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"/api/v2/users/{parentId}/identities/{id}");
            await _client.DeleteAsync(requestUri).ConfigureAwait(false);
        }
    }
}
