using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestCommentResource : IRequestCommentResource
    {
        private const string ResourceUri = "api/v2/requests/{0}/comments";
        private readonly IZendeskApiClient _apiClient;

        public RequestCommentResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TicketComment> GetAsync(long id, long parentId)
        {
            using (var client = _apiClient.CreateClient(string.Format(ResourceUri, parentId)))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<TicketCommentResponse>()).Item;
            }
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(ResourceUri, parentId)).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketCommentListResponse>();
            }
        }
    }
}
