using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentityResource : ZendeskResource<UserIdentity>, IUserIdentityResource
    {
        protected override string ResourceUri => @"/api/v2/users/{0}/identities";

        public UserIdentityResource(IRestClient client)
        {
            Client = client;
        }

        public IListResponse<UserIdentity> GetAll(long id)
        {
            return GetAllAsync(id).Result;
        }

        public async Task<IListResponse<UserIdentity>> GetAllAsync(long id)
        {
            return await GetAllAsync<UserIdentityListResponse>(id).ConfigureAwait(false);
        }

        public IResponse<UserIdentity> Post(UserIdentityRequest request)
        {
            return PostAsync(request).Result;
        }

        public async Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request)
        {
            return await PostAsync<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId).ConfigureAwait(false);
        }

        public IResponse<UserIdentity> Put(UserIdentityRequest request)
        {
            return PutAsync(request).Result;
        }

        public async Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request)
        {
            return await PutAsync<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId).ConfigureAwait(false);
        }

        public void Delete(long id, long parentId)
        {
            DeleteAsync(id, parentId).Wait();
        }

        public async Task DeleteAsync(long id, long parentId)
        {
            await base.DeleteAsync(id, parentId).ConfigureAwait(false);
        }
    }
}
