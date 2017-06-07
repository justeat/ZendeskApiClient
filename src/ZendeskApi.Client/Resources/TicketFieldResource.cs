using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldResource : ITicketFieldResource
    {
        private const string ResourceUri = "/api/v2/ticket_fields";
        private readonly IZendeskApiClient _apiClient;

        public TicketFieldResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TicketField> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<TicketFieldResponse>()).Item;
            }
        }

        public async Task<IListResponse<TicketField>> GetAllAsync()
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketFieldListResponse>();
            }
        }
    }
}
