using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketCommentResource : ITicketCommentResource
    {
        private const string ResourceUri = "api/v2/tickets/{0}/comments";
        private readonly IZendeskApiClient _apiClient;

        public TicketCommentResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
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
