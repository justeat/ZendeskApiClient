using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Extensions;
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

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(TicketsResource).Name + ": {0}");

        public TicketsResource(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }


        #region List Tickets
        public async Task<IPagination<TicketResponse>> ListAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "ListAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri, pager).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                .WithResponse(response)
                                .WithHelpDocsLink("core/tickets#list-tickets")
                                .Build();
                }

                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }
        
        public async Task<IPagination<TicketResponse>> ListForOrganizationAsync(long organizationId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListForOrganizationAsync({organizationId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(OrganizationResourceUriFormat, organizationId), pager).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Tickets in organization {0} not found", organizationId);
                    return null;
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithHelpDocsLink("core/tickets#list-tickets")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }

        public async Task<IPagination<TicketResponse>> ListRequestedByAsync(long userId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListRequestedByAsync({userId})"))
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("requested", pager).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Requested ticketsResponse for user {0} not found", userId);
                    return null;
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithHelpDocsLink("core/tickets#list-tickets")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }

        public async Task<IPagination<TicketResponse>> ListCcdAsync(long userId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListCcdAsync({userId})"))
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("ccd", pager).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("CCD ticketsResponse for user {0} not found", userId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithHelpDocsLink("core/tickets#list-tickets")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }

        public async Task<IPagination<TicketResponse>> ListAssignedToAsync(long userId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListAssignedToAsync({userId})"))
            using (var client = _apiClient.CreateClient(string.Format(UserResourceUriFormat, userId)))
            {
                var response = await client.GetAsync("assigned", pager).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Assigned ticketsResponse for user {0} not found", userId);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithHelpDocsLink("core/tickets#list-tickets")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }
        #endregion


        #region Show Tickets
        public async Task<TicketResponseContainer> GetAsync(long ticketId)
        {
            using (_loggerScope(_logger, $"GetAsync({ticketId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(ticketId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("TicketResponse {0} not found", ticketId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<TicketResponseContainer>();
            }
        }

        public async Task<IPagination<TicketResponse>> GetAsync(long[] ticketIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllAsync({ZendeskFormatter.ToCsv(ticketIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ticketIds)}", pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<TicketListResponse>();
            }
        }
        #endregion


        #region Create Tickets
        public async Task<TicketResponseContainer> CreateAsync(TicketCreateRequest ticket)
        {
            using (_loggerScope(_logger, "CreateAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, new TicketRequest<TicketCreateRequest>(ticket)).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithExpectedHttpStatus(HttpStatusCode.Created)
                                    .WithHelpDocsLink("core/tickets#create-ticket")
                                    .Build(); 
                }

                return await response.Content.ReadAsAsync<TicketResponseContainer>();
            }
        }

        public async Task<JobStatusResponse> CreateAsync(IEnumerable<TicketCreateRequest> tickets)
        {
            using (_loggerScope(_logger, "CreateAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PostAsJsonAsync("create_many", new TicketListRequest<TicketCreateRequest>(tickets)).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithExpectedHttpStatus(HttpStatusCode.Created)
                                    .WithHelpDocsLink("core/tickets#create-many-tickets")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<JobStatusResponse>();
            }
        }
        #endregion


        #region Update Tickets
        public async Task<TicketResponseContainer> UpdateAsync(TicketUpdateRequest ticket)
        {
            using (_loggerScope(_logger, "UpdateAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(ticket.Id.ToString(), new TicketRequest<TicketUpdateRequest>(ticket)).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update ticketResponse as ticketResponse {0} cannot be found", ticket.Id);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithHelpDocsLink("core/tickets#update-ticket")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<TicketResponseContainer>();
            }
        }

        public async Task<JobStatusResponse> UpdateAsync(IEnumerable<TicketUpdateRequest> tickets)
        {
            using (_loggerScope(_logger, "PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync("update_many", new TicketListRequest<TicketUpdateRequest>(tickets)).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithHelpDocsLink("core/tickets#update-many-tickets")
                                    .Build();
                }

                return await response.Content.ReadAsAsync<JobStatusResponse>();
            }
        }
        #endregion


        #region Mark Ticket as Spam and Suspend Requester
        public async Task<bool> MarkTicketAsSpamAndSuspendRequester(long ticketId)
        {
            using (_loggerScope(_logger, "MarkTicketAsSpamAndSuspendRequester"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync($"{ticketId}/mark_as_spam", "{ }").ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot mark ticketResponse {0} as spam as the ticketResponse is not found", ticketId);
                    return false;
                }

                response.EnsureSuccessStatusCode();

                return true;
            }
        }

        public async Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds)
        {
            using (_loggerScope(_logger, $"MarkTicketAsSpamAndSuspendRequester({ZendeskFormatter.ToCsv(ticketIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync($"mark_many_as_spam?ids={ZendeskFormatter.ToCsv(ticketIds)}", "{ }").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<JobStatusResponse>();
            }
        }
        #endregion


        #region Delete Tickets
        public async Task DeleteAsync(long ticketId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({ticketId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(ticketId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                                    .WithResponse(response)
                                    .WithExpectedHttpStatus(HttpStatusCode.NoContent)
                                    .WithHelpDocsLink("core/tickets#delete-ticket")
                                    .Build();
                }
            }
        }

        //TODO: Bulk Delete
        #endregion
    }
}
