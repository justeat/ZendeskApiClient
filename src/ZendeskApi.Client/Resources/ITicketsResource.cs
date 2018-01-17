using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{   
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public interface ITicketsResource
    {
        #region List Tickets
        Task<IPagination<Ticket>> ListAsync(PagerParameters pager = null);
        Task<IPagination<Ticket>> ListForOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<IPagination<Ticket>> ListRequestedByAsync(long userId, PagerParameters pager = null);
        Task<IPagination<Ticket>> ListCcdAsync(long userId, PagerParameters pager = null);
        Task<IPagination<Ticket>> ListAssignedToAsync(long userId, PagerParameters pager = null);
        #endregion

        #region Show Tickets
        Task<TicketResponse> GetAsync(long ticketId);
        Task<IPagination<Ticket>> GetAsync(long[] ticketIds, PagerParameters pager = null);
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
