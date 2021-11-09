using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketAuditResource
    {
        Task<TicketAuditResponse> GetAllAsync(CursorPagerVariant pager = null, CancellationToken cancellationToken = default);
        Task<TicketAuditResponse> GetAllByTicketAsync(long ticketId, CancellationToken cancellationToken = default);
        Task<SingleTicketAuditResponse> Get(int ticketId, int auditId, CancellationToken cancellationToken = default);
    }
}