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
    public class UserResource : IUserResource
    {
        private readonly IRestClient _client;
        private const string ResourceUri = "/api/v2/users/";

        public UserResource(IRestClient client)
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

        public IResponse<User> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<UserResponse>(requestUri);
        }

        public async Task<IResponse<User>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<UserResponse>(requestUri).ConfigureAwait(false);
        }

        public IListResponse<User> GetAll(List<long> ids)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/show_many", $"ids={ZendeskFormatter.ToCsv(ids)}");
            return _client.Get<UserListResponse>(requestUri);
        }

        public async Task<IListResponse<User>> GetAllAsync(List<long> ids)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/show_many", $"ids={ZendeskFormatter.ToCsv(ids)}");
            return await _client.GetAsync<UserListResponse>(requestUri).ConfigureAwait(false);
        }

        public IResponse<User> Post(UserRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return _client.Post<UserResponse>(requestUri, request);
        }

        public async Task<IResponse<User>> PostAsync(UserRequest request)
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return await _client.PostAsync<UserResponse>(requestUri, request).ConfigureAwait(false);
        }

        public IResponse<User> Put(UserRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");

            return _client.Put<UserResponse>(requestUri, request);
        }

        public async Task<IResponse<User>> PutAsync(UserRequest request)
        {
            if (!request.Item.Id.HasValue || request.Item.Id <= 0)
                throw new ArgumentException("Item must exist in Zendesk");

            var requestUri = _client.BuildUri($"{ResourceUri}/{request.Item.Id}");
            return await _client.PutAsync<UserResponse>(requestUri, request).ConfigureAwait(false);
        }
    }
}
