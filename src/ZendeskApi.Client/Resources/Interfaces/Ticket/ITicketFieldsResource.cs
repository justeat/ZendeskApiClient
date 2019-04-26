using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldsResource
    {
        Task<IPagination<TicketField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketField> GetAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketField> CreateAsync(
            TicketField ticketField,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketField> UpdateAsync(
            TicketField ticketField,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
