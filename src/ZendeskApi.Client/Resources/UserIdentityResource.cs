using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserIdentityResource : IUserIdentityResource
    {
        private const string ResourceUri = "api/v2/users/{0}/identities";
        private readonly IZendeskApiClient _apiClient;

        public UserIdentityResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IListResponse<UserIdentity>> GetAllAsync(long id)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(ResourceUri, id)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserIdentityListResponse>();
            }
        }
        
        public async Task<UserIdentity> PostAsync(UserIdentityRequest request)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(string.Format(ResourceUri, request.Item.UserId), request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<UserIdentityResponse>()).Item;
            }
        }
        
        public async Task<UserIdentity> PutAsync(UserIdentityRequest request)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PutAsJsonAsync(string.Format(ResourceUri, request.Item.UserId), request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<UserIdentityResponse>()).Item;
            }
        }
        
        public Task DeleteAsync(long id, long parentId)
        {
            using (var client = _apiClient.CreateClient(string.Format(ResourceUri, parentId)))
            {
                return client.DeleteAsync(id.ToString());
            }
        }
    }
}
