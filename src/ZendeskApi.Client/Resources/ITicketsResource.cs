using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketsResource
    {
        Task<IPagination<TicketResponse>> GetAllAsync(PagerParameters pager = null);
        Task<IPagination<TicketResponse>> GetAllForOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<IPagination<TicketResponse>> GetAllRequestedForUserAsync(long userId, PagerParameters pager = null);
        Task<IPagination<TicketResponse>> GetAllCCDForUserAsync(long userId, PagerParameters pager = null);
        Task<IPagination<TicketResponse>> GetAllAssignedForUserAsync(long userId, PagerParameters pager = null);
        Task<TicketResponse> GetAsync(long ticketId);
        Task<IPagination<TicketResponse>> GetAllAsync(long[] ticketIds, PagerParameters pager = null);
        Task<TicketResponse> CreateAsync(TicketCreateRequest ticket);
        Task<JobStatusResponse> CreateAsync(IEnumerable<TicketCreateRequest> tickets);
        Task<TicketResponse> UpdateAsync(TicketUpdateRequest ticket);
        Task<JobStatusResponse> UpdateAsync(IEnumerable<TicketUpdateRequest> tickets);
        Task<bool> MarkTicketAsSpamAndSuspendRequester(long ticketId);
        Task<JobStatusResponse> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds);
        Task DeleteAsync(long ticketId);
    }
}
