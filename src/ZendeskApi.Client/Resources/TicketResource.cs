using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketResource : ITicketResource
    {
        private const string ResourceUri = "/api/v2/tickets";
        private readonly IZendeskApiClient _apiClient;

        public TicketResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<IResponse<Ticket>> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketResponse>();
            }
        }

        public async Task<IListResponse<Ticket>> GetAllAsync(List<long> ids)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ids)}").ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }

        public async Task<IResponse<Ticket>> PutAsync(TicketRequest request)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketResponse>();
            }
        }

        public async Task<IResponse<Ticket>> PostAsync(TicketRequest request)
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return await response.Content.ReadAsAsync<TicketResponse>();
                }

                return null;
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
