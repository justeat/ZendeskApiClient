using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketCommentsResource : AbstractBaseResource<TicketCommentsResource>, 
        ITicketCommentsResource
    {
        private const string ResourceUri = "api/v2/tickets/{0}/comments";

        private readonly ITicketsResource _ticketsResource;

        public TicketCommentsResource(
            IZendeskApiClient apiClient, 
            ILogger logger)
            : base(apiClient, logger, "ticket_comments")
        {
            _ticketsResource = new TicketsResource(apiClient, logger);
        }

        public async Task<TicketCommentsListResponse> ListAsync(
            long ticketId, 
            PagerParameters pager = null)
        {
            return await GetAsync<TicketCommentsListResponse>(
                string.Format(ResourceUri, ticketId),
                "list-comments",
                $"ListAsync({ticketId})",
                pager);
        }

        public async Task AddComment(
            long ticketId, 
            TicketComment ticketComment)
        {
            var ticket = new TicketUpdateRequest(ticketId)
            {
                Comment = ticketComment
            };

            await _ticketsResource
                .UpdateAsync(ticket)
                .ConfigureAwait(false);
        }
    }
}
