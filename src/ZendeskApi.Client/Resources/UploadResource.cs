using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class UploadResource : ZendeskResource<Upload>, IUploadResource
    {
        private const string ResourceUri = "/api/v2/uploads";

        public UploadResource(ZendeskOptions options) : base(options) { }

        public Task DeleteAsync(string token)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                return client.DeleteAsync(token);
            }
        }

        public async Task<IResponse<Upload>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UploadResponse>();
            }
        }

        public async Task<IResponse<Upload>> PostAsync(UploadRequest request)
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.PostAsJsonAsync($"{ResourceUri}?filename={request.Item.FileName}{request.Token ?? string.Empty}", request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<UploadResponse>();
            }
        }
    }
}
