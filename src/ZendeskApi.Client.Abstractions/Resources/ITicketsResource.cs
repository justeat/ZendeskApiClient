using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketsResource
    {
        Task<IPagination<Ticket>> GetAllAsync(PagerParameters pager = null);
        Task<IPagination<Ticket>> GetAllForOrganizationAsync(long organizationId, PagerParameters pager = null);
        Task<IPagination<Ticket>> GetAllRequestedForUserAsync(long userId, PagerParameters pager = null);
        Task<IPagination<Ticket>> GetAllCCDForUserAsync(long userId, PagerParameters pager = null);
        Task<IPagination<Ticket>> GetAllAssignedForUserAsync(long userId, PagerParameters pager = null);
        Task<Ticket> GetAsync(long ticketId);
        Task<IPagination<Ticket>> GetAllAsync(long[] ticketIds, PagerParameters pager = null);
        Task<Ticket> PostAsync(Ticket ticket);
        Task<JobStatus> PostAsync(IEnumerable<Ticket> tickets);
        Task<Ticket> PutAsync(Ticket ticket);
        Task<JobStatus> PutAsync(IEnumerable<Ticket> tickets);
        Task<bool> MarkTicketAsSpamAndSuspendRequester(long ticketId);
        Task<JobStatus> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds);
        Task DeleteAsync(long ticketId);
    }
}
