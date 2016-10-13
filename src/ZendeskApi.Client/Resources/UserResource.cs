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
    public class UserResource : ZendeskResource<User>, IUserResource
    {
        private const string ResourceUri = "/api/v2/users/";

        public UserResource(IRestClient client)
        {
            Client = client;
        }

        public void Delete(long id)
        {
            ValidateRequest(id);
            Delete($"{ResourceUri}/{id}");
        }

        public IResponse<User> Get(long id)
        {
            return Get<UserResponse>($"{ResourceUri}/{id}");
        }

        public async Task<IResponse<User>> GetAsync(long id)
        {
            return await GetAsync<UserResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public IListResponse<User> GetAll(List<long> ids)
        {
            return GetAll<UserListResponse>($"{ResourceUri}/show_many", ids);
        }

        public async Task<IListResponse<User>> GetAllAsync(List<long> ids)
        {
            return await GetAllAsync<UserListResponse>($"{ResourceUri}/show_many", ids).ConfigureAwait(false);
        }

        public IResponse<User> Post(UserRequest request)
        {
            return Post<UserRequest, UserResponse>(request, ResourceUri);
        }

        public async Task<IResponse<User>> PostAsync(UserRequest request)
        {
            return await PostAsync<UserRequest, UserResponse>(request, ResourceUri).ConfigureAwait(false);
        }

        public IResponse<User> Put(UserRequest request)
        {
            ValidateRequest(request);
            return Put<UserRequest, UserResponse>(request, $"{ResourceUri}/{request.Item.Id}");
        }

        public async Task<IResponse<User>> PutAsync(UserRequest request)
        {
            ValidateRequest(request);
            return await PutAsync<UserRequest, UserResponse>(request, 
                $"{ResourceUri}/{request.Item.Id}").ConfigureAwait(false);
        }
    }
}
