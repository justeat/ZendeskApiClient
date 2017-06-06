using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private const string BatchResourceUri = "/api/v2/tickets/create_many";
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
                
                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" + 
                        Environment.NewLine + 
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return await response.Content.ReadAsAsync<TicketResponse>();
            }
        }

        public async Task<JobStatus> PostAsync(TicketsRequest request)
        {
            using (var client = _apiClient.CreateClient("/"))
            {
                var response = await client.PostAsJsonAsync(BatchResourceUri, request).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return (await response.Content.ReadAsAsync<JobStatusResponse>()).Item;
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
