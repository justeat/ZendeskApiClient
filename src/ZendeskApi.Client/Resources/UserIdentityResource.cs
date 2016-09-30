using System;
using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentityResource : ZendeskResource<UserIdentity>, IUserIdentityResource
    {
        private const string ResourceUri = "/api/v2/users/{0}/identities";

        public UserIdentityResource(IRestClient client)
        {
            Client = client;
        }

        public IListResponse<UserIdentity> GetAll(long id)
        {
            return GetAll<UserIdentityListResponse>(string.Format(ResourceUri, id));
        }

        public async Task<IListResponse<UserIdentity>> GetAllAsync(long id)
        {
            return await GetAllAsync<UserIdentityListResponse>(string.Format(ResourceUri, id)).ConfigureAwait(false);
        }

        public IResponse<UserIdentity> Post(UserIdentityRequest request)
        {
            return Post<UserIdentityRequest, UserIdentityResponse>(request, 
                string.Format(ResourceUri, request.Item.UserId));
        }

        public async Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request)
        {
            return await PostAsync<UserIdentityRequest, UserIdentityResponse>(request, 
                string.Format(ResourceUri, request.Item.UserId)).ConfigureAwait(false);
        }

        public IResponse<UserIdentity> Put(UserIdentityRequest request)
        {
            ValidateRequest(request);
            return Put<UserIdentityRequest, UserIdentityResponse>(request, 
                string.Format(ResourceUri, request.Item.UserId));
        }

        public async Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request)
        {
            return await PutAsync<UserIdentityRequest, UserIdentityResponse>(request,
                string.Format(ResourceUri, request.Item.UserId)).ConfigureAwait(false);
        }

        public void Delete(long id, long parentId)
        {
            ValidateRequest(id);
            Delete($"{string.Format(ResourceUri, parentId)}/{id}");
        }

        public async Task DeleteAsync(long id, long parentId)
        {
            ValidateRequest(id);
            await DeleteAsync($"{string.Format(ResourceUri, parentId)}/{id}").ConfigureAwait(false); ;
        }
    }
}
