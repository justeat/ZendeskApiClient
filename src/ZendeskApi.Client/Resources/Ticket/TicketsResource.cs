using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class TicketsResource : AbstractBaseResource<TicketsResource>,
        ITicketsResource
    {
        private const string ResourceUri = "api/v2/tickets";

        private const string OrganizationResourceUriFormat = "api/v2/organizations/{0}/tickets";
        private const string UserResourceUriFormat = "api/v2/users/{0}/tickets";

        public TicketsResource(IZendeskApiClient apiClient, ILogger logger)
            : base(apiClient, logger, "tickets")
        { }

        #region List Tickets
        public async Task<IPagination<Ticket>> ListAsync(PagerParameters pager = null)
        {
            return await GetAsync<TicketsListResponse>(
                ResourceUri,
                "list-tickets",
                "ListAsync",
                pager);
        }
        
        public async Task<IPagination<Ticket>> ListForOrganizationAsync(long organizationId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                string.Format(OrganizationResourceUriFormat, organizationId),
                "list-tickets",
                $"ListForOrganizationAsync({organizationId})",
                $"Tickets in organization {organizationId} not found",
                pager);
        }

        public async Task<IPagination<Ticket>> ListRequestedByAsync(long userId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/requested",
                "list-tickets",
                $"ListRequestedByAsync({userId})",
                $"Requested ticketsResponse for user {userId} not found",
                pager);
        }

        public async Task<IPagination<Ticket>> ListCcdAsync(long userId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/ccd",
                "list-tickets",
                $"ListCcdAsync({userId})",
                $"CCD ticketsResponse for user {userId} not found",
                pager);
        }

        public async Task<IPagination<Ticket>> ListAssignedToAsync(long userId, PagerParameters pager = null)
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/assigned",
                "list-tickets",
                $"ListAssignedToAsync({userId})",
                $"Assigned ticketsResponse for user {userId} not found",
                pager);
        }
        #endregion

        #region Show Tickets
        public async Task<TicketResponse> GetAsync(long ticketId)
        {
            return await GetWithNotFoundCheckAsync<TicketResponse>(
                $"{ResourceUri}/{ticketId}",
                "show-ticket",
                $"GetAsync({ticketId})",
                $"TicketResponse {ticketId} not found");
        }

        public async Task<IPagination<Ticket>> GetAsync(long[] ticketIds, PagerParameters pager = null)
        {
            return await GetAsync<TicketsListResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                "show-multiple-tickets",
                $"GetAllAsync({ZendeskFormatter.ToCsv(ticketIds)})",
                pager);
        }
        #endregion

        #region Create Tickets
        public async Task<TicketResponse> CreateAsync(TicketCreateRequest ticket)
        {
            return await CreateAsync<TicketResponse, TicketRequest<TicketCreateRequest>>(
                ResourceUri,
                new TicketRequest<TicketCreateRequest>(ticket),
                "create-ticket");
        }

        public async Task<JobStatusResponse> CreateAsync(IEnumerable<TicketCreateRequest> tickets)
        {
            var response = await CreateAsync<SingleJobStatusResponse, TicketListRequest<TicketCreateRequest>>(
                $"{ResourceUri}/create_many",
                new TicketListRequest<TicketCreateRequest>(tickets),
                "create-many-tickets",
                HttpStatusCode.OK);

            return response?
                .JobStatus;
        }
        #endregion

        #region Update Tickets
        public async Task<TicketResponse> UpdateAsync(TicketUpdateRequest ticket)
        {
            return await UpdateWithNotFoundCheckAsync<TicketResponse, TicketRequest<TicketUpdateRequest>>(
                $"{ResourceUri}/{ticket.Id}",
                new TicketRequest<TicketUpdateRequest>(ticket),
                "update-ticket",
                $"Cannot update ticketResponse as ticketResponse {ticket.Id} cannot be found");
        }

        public async Task<JobStatusResponse> UpdateAsync(IEnumerable<TicketUpdateRequest> tickets)
        {
            return await UpdateAsync<JobStatusResponse, TicketListRequest<TicketUpdateRequest>>(
                $"{ResourceUri}/update_many.json",
                new TicketListRequest<TicketUpdateRequest>(tickets),
                "update-many-tickets");
        }
        #endregion

        #region Mark Ticket as Spam and Suspend Requester
        public async Task<bool> MarkTicketAsSpamAndSuspendRequester(long ticketId)
        {
            var response = await UpdateWithNotFoundCheckAsync<HttpResponseMessage, object>(
                $"{ResourceUri}/{ticketId}/mark_as_spam",
                new { },
                "mark-ticket-as-spam-and-suspend-requester",
                $"Cannot mark ticketResponse {ticketId} as spam as the ticketResponse is not found",
                "MarkTicketAsSpamAndSuspendRequester");

            return response != null;
        }

        public async Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds)
        {
            return await UpdateAsync<JobStatusResponse, object>(
                $"{ResourceUri}/mark_many_as_spam.json?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                new {},
                "bulk-mark-tickets-as-spam",
                $"MarkTicketAsSpamAndSuspendRequester({ZendeskFormatter.ToCsv(ticketIds)})");
        }
        #endregion

        #region Delete Tickets
        public async Task DeleteAsync(long ticketId)
        {
            await DeleteAsync(
                ResourceUri,
                ticketId,
                "delete-ticket");
        }

        public async Task DeleteAsync(IEnumerable<long> ticketIds)
        {
            var ids = ticketIds
                .ToList();

            await DeleteAsync(
                $"{ResourceUri}/destroy_many.json",
                ids,
                "bulk-delete-tickets");
        }
        #endregion
    }
}
