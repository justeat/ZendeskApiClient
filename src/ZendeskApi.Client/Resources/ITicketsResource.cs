using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.Responses;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.Resources
{   
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public interface ITicketsResource
    {
        #region List Tickets
        Task<IPagination<TicketResponse>> ListAsync(PagerParameters pager = null);
        Task<IPagination<TicketResponse>> ListForOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<IPagination<TicketResponse>> ListRequestedByAsync(long userId, PagerParameters pager = null);
        Task<IPagination<TicketResponse>> ListCcdAsync(long userId, PagerParameters pager = null);
        Task<IPagination<TicketResponse>> ListAssignedToAsync(long userId, PagerParameters pager = null);
        #endregion

        #region Show Tickets
        Task<TicketResponse> GetAsync(long ticketId);
        Task<IPagination<TicketResponse>> GetAsync(long[] ticketIds, PagerParameters pager = null);
        #endregion

        #region Create Tickets
        Task<TicketResponse> CreateAsync(TicketCreateRequest ticket);
        Task<JobStatusResponse> CreateAsync(IEnumerable<TicketCreateRequest> tickets);
        #endregion

        #region Update Tickets
        Task<TicketResponse> UpdateAsync(TicketUpdateRequest ticket);
        Task<JobStatusResponse> UpdateAsync(IEnumerable<TicketUpdateRequest> tickets);
        #endregion

        #region Mark Ticket as Spam and Suspend Requester
        Task<bool> MarkTicketAsSpamAndSuspendRequester(long ticketId);
        Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds);
        #endregion

        #region Delete Tickets
        Task DeleteAsync(long ticketId);
        #endregion
    }
}
