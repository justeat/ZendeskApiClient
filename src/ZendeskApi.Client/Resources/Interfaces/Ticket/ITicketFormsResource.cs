using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public interface ITicketFormsResource
    {
        Task<IPagination<TicketForm>> GetAllAsync(
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketForm> GetAsync(
            long ticketformId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagination<TicketForm>> GetAllAsync(
            long[] ticketFormsIds, 
            PagerParameters pager = null,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketForm> CreateAsync(
            TicketForm ticketForm,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<TicketForm> UpdateAsync(
            TicketForm ticketForm,
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(
            long ticketFormId,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
