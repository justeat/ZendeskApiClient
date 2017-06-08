using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public class TicketsResource : ITicketsResource
    {
        private const string ResourceUri = "api/v2/tickets";

        private const string OrganizationResourceUriFormat = "api/v2/organizations/{0}/tickets";
        private const string UserResourceUriFormat = "api/v2/users/{0}/tickets";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>("TicketsResource: {0}");

        public TicketsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }
        
        public async Task<IEnumerable<Ticket>> GetAllForOrganizationAsync(long organizationId)
        {
            using (_loggerScope(_logger, $"GetAllForOrganizationAsync({organizationId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganizationResourceUriFormat, organizationId)).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllRequestedForUserAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAllRequestedForUserAsync({userId})"))
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("requested").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllCCDForUserAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAllCCDForUserAsync({userId})"))
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("ccd").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllAssignedForUserAsync(long userId)
        {
            using (_loggerScope(_logger, $"GetAllAssignedForUserAsync({userId})"))
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("assigned").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }
        
        public async Task<Ticket> GetAsync(long ticketId)
        {
            using (_loggerScope(_logger, $"GetAsync({ticketId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(ticketId.ToString()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketResponse>()).Item;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(long[] ticketIds)
        {
            using (_loggerScope(_logger, $"GetAllAsync({ZendeskFormatter.ToCsv(ticketIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ticketIds)}").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketsResponse>()).Item;
            }
        }

        public async Task<Ticket> PostAsync(Ticket ticket)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new TicketRequest { Item = ticket }).ConfigureAwait(false);

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

        public async Task<JobStatus> PostAsync(IEnumerable<Ticket> tickets)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PostAsJsonAsync("create_many", new TicketsRequest { Item = tickets }).ConfigureAwait(false);

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

        public async Task<Ticket> PutAsync(Ticket ticket)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(ticket.Id.ToString(), new TicketRequest { Item = ticket }).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<TicketResponse>()).Item;
            }
        }

        public async Task<JobStatus> PutAsync(IEnumerable<Ticket> tickets)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync("update_many", new TicketsRequest { Item = tickets }).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<JobStatusResponse>()).Item;
            }
        }

        public async Task MarkTicketAsSpamAndSuspendRequester(long ticketId)
        {
            using (_loggerScope(_logger, $"MarkTicketAsSpamAndSuspendRequester"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync($"{ticketId}/mark_as_spam", "{ }").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<JobStatus> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds)
        {
            using (_loggerScope(_logger, $"MarkTicketAsSpamAndSuspendRequester({ZendeskFormatter.ToCsv(ticketIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync($"mark_many_as_spam?ids={ZendeskFormatter.ToCsv(ticketIds)}", "{ }").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return (await response.Content.ReadAsAsync<JobStatusResponse>()).Item;
            }
        }

        public async Task DeleteAsync(long ticketId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({ticketId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(ticketId.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new HttpRequestException(
                        $"Status code retrieved was {response.StatusCode} and not a 204 as expected" +
                        Environment.NewLine +
                        "See: https://developer.zendesk.com/rest_api/docs/core/tickets#delete-ticket");
                }
            }
        }
    }
}
