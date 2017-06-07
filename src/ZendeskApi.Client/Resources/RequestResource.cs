using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestResource : IRequestResource
    {
        private const string ResourceUri = "/api/v2/requests";
        private readonly IZendeskApiClient _apiClient;

        public RequestResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Request> GetAsync(long id)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
            }
        }

        public async Task<Request> GetAsync(IEnumerable<TicketStatus> requestedStatuses)
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                // TODO: ngm make nicer
                var query = $"status={string.Join(",", requestedStatuses).ToLower()}";
                var response = await client.GetAsync($"{ResourceUri}?{query}").ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
            }
        }

        public async Task<Request> PutAsync(RequestRequest request)
        {
            using (var client = _apiClient.CreateClient(ResourceUri + "/"))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
            }
        }

        public async Task<Request> PostAsync(RequestRequest request)
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);
                return (await response.Content.ReadAsAsync<RequestResponse>()).Item;
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
