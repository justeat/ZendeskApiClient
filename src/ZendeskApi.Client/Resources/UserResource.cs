using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UserResource : IUserResource
    {
        private const string ResourceUri = "/api/v2/users/";
        private readonly IZendeskApiClient _apiClient;

        public UserResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IResponse<User>> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }
        
        public async Task<IListResponse<User>> GetAllAsync(List<long> ids)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ids)}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserListResponse>();
            }
        }
        
        public async Task<IResponse<User>> PostAsync(UserRequest request)
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }
        
        public async Task<IResponse<User>> PutAsync(UserRequest request)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UserResponse>();
            }
        }

        public Task DeleteAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                return client.DeleteAsync(id.ToString());
            }
        }
    }
}
