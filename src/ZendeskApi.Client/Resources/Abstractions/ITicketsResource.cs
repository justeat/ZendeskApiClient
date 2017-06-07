using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Requests;

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
        Task<Ticket> PostAsync(TicketRequest request);
        Task<JobStatus> PostAsync(TicketsRequest request);
        Task<Ticket> PutAsync(TicketRequest request);
        Task<JobStatus> PutAsync(TicketsRequest request);
        Task MarkTicketAsSpamAndSuspendRequester(long ticketId);
        Task<JobStatus> MarkTicketAsSpamAndSuspendRequester(long[] ticketIds);
        Task DeleteAsync(long ticketId);
    }
}
