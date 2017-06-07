using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormResource : ITicketFormResource
    {
        private const string ResourceUri = "/api/v2/ticket_forms";
        private readonly IZendeskApiClient _apiClient;

        public TicketFormResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<TicketForm> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<TicketFormResponse>()).Item;
            }
        }

        public async Task<IListResponse<TicketForm>> GetAllAsync()
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketFormListResponse>();
            }
        }
    }
}
