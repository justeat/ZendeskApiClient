using System.Collections.Generic;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface IDeletedTicketsResource
    {
        Task<IPagination<Ticket>> ListAsync(PagerParameters pager = null);
        Task RestoreAsync(long ticketId);
        Task RestoreAsync(IEnumerable<long> ticketIds);
        Task<JobStatusResponse> PurgeAsync(long ticketId);
        Task<JobStatusResponse> PurgeAsync(IEnumerable<long> ticketIds);
    }
}