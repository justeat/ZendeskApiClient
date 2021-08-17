using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFieldsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<TicketField>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<TicketFieldsCursorResponse> GetAllAsync(
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<TicketField> GetAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default);

        Task<TicketField> CreateAsync(
            TicketField ticketField,
            CancellationToken cancellationToken = default);

        Task<TicketField> UpdateAsync(
            TicketField ticketField,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long ticketFieldId,
            CancellationToken cancellationToken = default);
    }
}
