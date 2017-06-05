using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentityResource : ZendeskResource<UserIdentity>, IUserIdentityResource
    {
        private const string ResourceUri = "/api/v2/users/{0}/identities";

        public UserIdentityResource(ZendeskOptions options) : base(options) { }
        
        public async Task<IListResponse<UserIdentity>> GetAllAsync(long id)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(string.Format(ResourceUri, id)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserIdentityListResponse>();
            }
        }
        
        public async Task<IResponse<UserIdentity>> PostAsync(UserIdentityRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.PostAsJsonAsync(string.Format(ResourceUri, request.Item.UserId), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserIdentityResponse>();
            }
        }
        
        public async Task<IResponse<UserIdentity>> PutAsync(UserIdentityRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.PutAsJsonAsync(string.Format(ResourceUri, request.Item.UserId), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserIdentityResponse>();
            }
        }
        
        public Task DeleteAsync(long id, long parentId)
        {
            using (var client = CreateZendeskClient(string.Format(ResourceUri, parentId) + "/"))
            {
                return client.DeleteAsync(id.ToString());
            }
        }
    }
}
