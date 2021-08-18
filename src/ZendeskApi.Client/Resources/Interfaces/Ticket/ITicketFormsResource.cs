using System;
using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormsResource
    {
        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<TicketForm>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<TicketForm>> GetAllAsync(
            CursorPager pager = null,
            CancellationToken cancellationToken = default);

        Task<TicketForm> GetAsync(
            long ticketformId,
            CancellationToken cancellationToken = default);

        [Obsolete("Use `GetAllAsync` with CursorPager parameter instead.")]
        Task<IPagination<TicketForm>> GetAllAsync(
            long[] ticketFormsIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default);

        Task<ICursorPagination<TicketForm>> GetAllAsync(
            long[] ticketFormsIds,
            CursorPager pager,
            CancellationToken cancellationToken = default);

        Task<TicketForm> CreateAsync(
            TicketForm ticketForm,
            CancellationToken cancellationToken = default);

        Task<TicketForm> UpdateAsync(
            TicketForm ticketForm,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            long ticketFormId,
            CancellationToken cancellationToken = default);
    }
}
