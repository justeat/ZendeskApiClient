using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketCommentsResource : ITicketCommentsResource
    {
        private const string ResourceUri = "api/v2/tickets/{0}/comments";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = 
            LoggerMessage.DefineScope<string>(typeof(TicketCommentsResource).Name + ": {0}");

        private readonly ITicketsResource _ticketsResource;

        public TicketCommentsResource(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;

            _ticketsResource = new TicketsResource(apiClient, logger);
        }

        public async Task<TicketCommentListResponse> ListAsync(long ticketId, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"ListAsync({ticketId})"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(string.Format(ResourceUri, ticketId), pager).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                return await  response.Content.ReadAsAsync<TicketCommentListResponse>();
            }
        }

        public async Task AddComment(long ticketId, TicketComment ticketComment)
        {
            var ticket = new TicketUpdateRequest(ticketId)
            {
                Comment = ticketComment
            };

            await _ticketsResource.UpdateAsync(ticket).ConfigureAwait(false);
        }
    }
}
