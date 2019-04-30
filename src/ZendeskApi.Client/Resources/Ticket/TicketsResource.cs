using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
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

        public TicketsResource(
            IZendeskApiClient apiClient, 
            ILogger logger)
            : base(apiClient, logger, "tickets")
        { }

        #region GetAll Tickets
        public async Task<IPagination<Ticket>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<TicketsListResponse>(
                ResourceUri,
                "list-tickets",
                "ListAsync",
                pager,
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllForOrganizationAsync(
            long organizationId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                string.Format(OrganizationResourceUriFormat, organizationId),
                "list-tickets",
                $"ListForOrganizationAsync({organizationId})",
                $"Tickets in organization {organizationId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllRequestedByAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/requested",
                "list-tickets",
                $"ListRequestedByAsync({userId})",
                $"Requested ticketsResponse for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllCcdAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/ccd",
                "list-tickets",
                $"ListCcdAsync({userId})",
                $"CCD ticketsResponse for user {userId} not found",
                pager,
                cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAllAssignedToAsync(
            long userId,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<TicketsListResponse>(
                $"{string.Format(UserResourceUriFormat, userId)}/assigned",
                "list-tickets",
                $"ListAssignedToAsync({userId})",
                $"Assigned ticketsResponse for user {userId} not found",
                pager,
                cancellationToken);
        }
        #endregion

        #region List Tickets
        [Obsolete("Use `GetAllAsync` instead.")]
        public async Task<IPagination<Ticket>> ListAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAllAsync(
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllForOrganizationAsync` instead.")]
        public async Task<IPagination<Ticket>> ListForOrganizationAsync(
            long organizationId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAllForOrganizationAsync(
                organizationId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllRequestedByAsync` instead.")]
        public async Task<IPagination<Ticket>> ListRequestedByAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAllRequestedByAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllCcdAsync` instead.")]
        public async Task<IPagination<Ticket>> ListCcdAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAllCcdAsync(
                userId,
                pager,
                cancellationToken);
        }

        [Obsolete("Use `GetAllAssignedToAsync` instead.")]
        public async Task<IPagination<Ticket>> ListAssignedToAsync(
            long userId, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAllAssignedToAsync(
                userId,
                pager,
                cancellationToken);
        }
        #endregion

        #region Show Tickets
        public async Task<TicketResponse> GetAsync(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetWithNotFoundCheckAsync<TicketResponse>(
                $"{ResourceUri}/{ticketId}",
                "show-ticket",
                $"GetAsync({ticketId})",
                $"TicketResponse {ticketId} not found",
                cancellationToken: cancellationToken);
        }

        public async Task<IPagination<Ticket>> GetAsync(
            long[] ticketIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<TicketsListResponse>(
                $"{ResourceUri}/show_many?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                "show-multiple-tickets",
                $"GetAllAsync({ZendeskFormatter.ToCsv(ticketIds)})",
                pager,
                cancellationToken: cancellationToken);
        }
        #endregion

        #region Create Tickets
        public async Task<TicketResponse> CreateAsync(
            TicketCreateRequest ticket,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateAsync<TicketResponse, TicketRequest<TicketCreateRequest>>(
                ResourceUri,
                new TicketRequest<TicketCreateRequest>(ticket),
                "create-ticket",
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> CreateAsync(
            IEnumerable<TicketCreateRequest> tickets,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await CreateAsync<SingleJobStatusResponse, TicketListRequest<TicketCreateRequest>>(
                $"{ResourceUri}/create_many",
                new TicketListRequest<TicketCreateRequest>(tickets),
                "create-many-tickets",
                HttpStatusCode.OK,
                cancellationToken: cancellationToken);

            return response?
                .JobStatus;
        }
        #endregion

        #region Update Tickets
        public async Task<TicketResponse> UpdateAsync(
            TicketUpdateRequest ticket,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateWithNotFoundCheckAsync<TicketResponse, TicketRequest<TicketUpdateRequest>>(
                $"{ResourceUri}/{ticket.Id}",
                new TicketRequest<TicketUpdateRequest>(ticket),
                "update-ticket",
                $"Cannot update ticketResponse as ticketResponse {ticket.Id} cannot be found",
                cancellationToken: cancellationToken);
        }

        public async Task<JobStatusResponse> UpdateAsync(
            IEnumerable<TicketUpdateRequest> tickets,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateAsync<JobStatusResponse, TicketListRequest<TicketUpdateRequest>>(
                $"{ResourceUri}/update_many.json",
                new TicketListRequest<TicketUpdateRequest>(tickets),
                "update-many-tickets",
                cancellationToken: cancellationToken);
        }
        #endregion

        #region Mark Ticket as Spam and Suspend Requester
        public async Task<bool> MarkTicketAsSpamAndSuspendRequester(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await UpdateWithNotFoundCheckAsync<HttpResponseMessage, object>(
                $"{ResourceUri}/{ticketId}/mark_as_spam",
                new { },
                "mark-ticket-as-spam-and-suspend-requester",
                $"Cannot mark ticketResponse {ticketId} as spam as the ticketResponse is not found",
                "MarkTicketAsSpamAndSuspendRequester",
                cancellationToken);

            return response != null;
        }

        public async Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(
            long[] ticketIds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await UpdateAsync<JobStatusResponse, object>(
                $"{ResourceUri}/mark_many_as_spam.json?ids={ZendeskFormatter.ToCsv(ticketIds)}",
                new {},
                "bulk-mark-tickets-as-spam",
                $"MarkTicketAsSpamAndSuspendRequester({ZendeskFormatter.ToCsv(ticketIds)})",
                cancellationToken);
        }
        #endregion

        #region Delete Tickets
        public async Task DeleteAsync(
            long ticketId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await DeleteAsync(
                ResourceUri,
                ticketId,
                "delete-ticket",
                cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(
            IEnumerable<long> ticketIds,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ids = ticketIds
                .ToList();

            await DeleteAsync(
                $"{ResourceUri}/destroy_many.json",
                ids,
                "bulk-delete-tickets",
                cancellationToken: cancellationToken);
        }
        #endregion
    }
}
