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
            return GetAll<UserIdentityListResponse>(id);
        }

        public async Task<IListResponse<UserIdentity>> GetAllAsync(long id)
        {
            return await GetAllAsync<UserIdentityListResponse>(id);
        }

        public IResponse<UserIdentity> Post(UserIdentityRequest request)
        {
            return Post<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId);
        }

        public async Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request)
        {
            return await PostAsync<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId);
        }

        public IResponse<UserIdentity> Put(UserIdentityRequest request)
        {
            return Put<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId);
        }

        public async Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request)
        {
            return await PutAsync<UserIdentityRequest, UserIdentityResponse>(request, request.Item.UserId);
        }

        public void Delete(long id, long parentId)
        {
            base.Delete(id, parentId);
        }

        public async Task DeleteAsync(long id, long parentId)
        {
            await base.DeleteAsync(id, parentId);
        }
    }
}
