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
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public class TicketResource : ITicketResource
    {
        private const string ResourceUri = "api/v2/tickets";

        private const string OrganizationResourceUriFormat = "api/v2/organizations/{organization_id}/tickets.json";
        private const string UserResourceUriFormat = "api/v2/{0}/tickets";

        private readonly IZendeskApiClient _apiClient;

        public TicketResource(IZendeskApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }
        
        public async Task<IEnumerable<Ticket>> GetAllForOrganizationAsync(long organizationId)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganizationResourceUriFormat, organizationId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllRequestedForUserAsync(long userId)
        {
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("requested").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllCCDForUserAsync(long userId)
        {
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("ccd").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllAssignedForUserAsync(long userId)
        {
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("assigned").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }
        
        public async Task<Ticket> GetAsync(long ticketId)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(ticketId.ToString()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(long[] ticketIds)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ticketIds)}").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<Ticket> PostAsync(TicketRequest request)
        {
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 201 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket");
                }

                return (await response.Content.ReadAsAsync<TicketResponse>()).Item;
            }
        }

        public async Task<JobStatus> PostAsync(TicketsRequest request)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PostAsJsonAsync("create_many", request).ConfigureAwait(false);

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

        public async Task<Ticket> PutAsync(TicketRequest request)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(request.Item.Id.ToString(), request).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketResponse>()).Item;
            }
        }

        public async Task<JobStatus> PutAsync(TicketsRequest request)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync("update_many", request).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<JobStatusResponse>()).Item;
            }
        }

        public async Task MarkTicketAsSpanAndSuspendRequester(long ticketId)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync($"{ticketId}/mark_as_spam", "{ }").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<JobStatus> MarkTicketAsSpanAndSuspendRequester(long[] ticketIds)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync($"mark_many_as_spam?ids={ZendeskFormatter.ToCsv(ticketIds)}", "{ }").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<JobStatusResponse>()).Item;
            }
        }

        public Task DeleteAsync(long ticketId)
        {
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                return client.DeleteAsync(ticketId.ToString());
            }
        }
    }
}
