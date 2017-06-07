using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UploadResource : IUploadResource
    {
        private const string ResourceUri = "/api/v2/uploads";
        private readonly IZendeskApiClient _apiClient;

        public UploadResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Task DeleteAsync(string token)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                return client.DeleteAsync(token);
            }
        }

        public async Task<Upload> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<UploadResponse>()).Item;
            }
        }

        public async Task<Upload> PostAsync(UploadRequest request)
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.PostAsJsonAsync($"{ResourceUri}?filename={request.Item.FileName}{request.Token ?? string.Empty}", request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<UploadResponse>()).Item;
            }
        }
    }
}
