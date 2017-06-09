using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketsResource
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<IEnumerable<Ticket>> GetAllForOrganizationAsync(long organizationId);
        Task<IEnumerable<Ticket>> GetAllRequestedForUserAsync(long userId);
        Task<IEnumerable<Ticket>> GetAllCCDForUserAsync(long userId);
        Task<IEnumerable<Ticket>> GetAllAssignedForUserAsync(long userId);
        Task<Ticket> GetAsync(long ticketId);
        Task<IEnumerable<Ticket>> GetAllAsync(long[] ticketIds);
        Task<Ticket> PostAsync(Ticket ticket);
        Task<JobStatus> PostAsync(IEnumerable<Ticket> tickets);
        Task<Ticket> PutAsync(Ticket ticket);
        Task<JobStatus> PutAsync(IEnumerable<Ticket> tickets);
        Task<bool> MarkTicketAsSpamAndSuspendRequester(long ticketId);
        Task<JobStatus> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds);
        Task DeleteAsync(long ticketId);
    }
}
