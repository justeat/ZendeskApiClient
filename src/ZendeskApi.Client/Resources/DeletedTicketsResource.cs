using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    /// <summary>
    /// <see cref="https://developer.zendesk.com/rest_api/docs/core/tickets"/>
    /// </summary>
    public class DeletedTicketsResource : IDeletedTicketsResource
    {
        private const string ResourceUri = "api/v2/deleted_tickets";
        
        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private readonly Func<ILogger, string, IDisposable> _loggerScope = LoggerMessage.DefineScope<string>(typeof(TicketsResource).Name + ": {0}");

        public DeletedTicketsResource(IZendeskApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<Ticket>> ListAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "ListAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"/{ResourceUri}.json", pager).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithHelpDocsLink("core/tickets#show-deleted-tickets")
                        .Build();
                }

                return await response.Content.ReadAsAsync<TicketsListResponse>();
            }
        }

        public async Task RestoreAsync(long ticketId)
        {
            using (_loggerScope(_logger, "RestoreAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client
                    .PutAsync(string.Format($"{ticketId}/restore.json", ticketId), new StringContent(string.Empty))
                    .ConfigureAwait(false);
                
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithExpectedHttpStatus(HttpStatusCode.NoContent)
                        .WithHelpDocsLink("core/tickets#restore-a-previously-deleted-ticket")
                        .Build();
                }
            }
        }
        
        public async Task RestoreAsync(IEnumerable<long> ticketIds)
        {
            if (ticketIds == null)
            {
                throw new ArgumentNullException($"{nameof(ticketIds)} must not be null", nameof(ticketIds));
            }

            var ticketIdList = ticketIds.ToList();

            if (ticketIdList.Count == 0 || ticketIdList.Count > 100)
            {
                throw new ArgumentException($"{nameof(ticketIds)} must have [0..100] elements", nameof(ticketIds));
            }

            var ticketIdsString = ZendeskFormatter.ToCsv(ticketIdList);

            using (_loggerScope(_logger, $"RestoreManyAsync({ticketIdsString})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client
                    .PutAsync($"restore_many?ids={ticketIdsString}", new StringContent(string.Empty))
                    .ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithExpectedHttpStatus(HttpStatusCode.NoContent)
                        .WithHelpDocsLink("core/tickets#restore-previously-deleted-tickets-in-bulk")
                        .Build();
                }
            }
        }

        public async Task<JobStatusResponse> PurgeAsync(long ticketId)
        {
            using (_loggerScope(_logger, $"PurgeAsync({ticketId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync($"{ticketId}.json").ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithExpectedHttpStatus(HttpStatusCode.NoContent)
                        .WithHelpDocsLink("core/tickets#delete-ticket-permanently")
                        .Build();
                }

                return await response.Content.ReadAsAsync<JobStatusResponse>();
            }
        }

        public async Task<JobStatusResponse> PurgeAsync(IEnumerable<long> ticketIds)
        {
            if (ticketIds == null)
            {
                throw new ArgumentNullException($"{nameof(ticketIds)} must not be null", nameof(ticketIds));
            }

            var ticketIdList = ticketIds.ToList();

            if (ticketIdList.Count == 0 || ticketIdList.Count > 100)
            {
                throw new ArgumentException($"{nameof(ticketIds)} must have [0..100] elements", nameof(ticketIds));
            }

            var ticketIdsString = ZendeskFormatter.ToCsv(ticketIdList);

            using (_loggerScope(_logger, $"PurgeAsync({ticketIdsString})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync($"destroy_many?ids={ticketIdsString}").ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw await new ZendeskRequestExceptionBuilder()
                        .WithResponse(response)
                        .WithExpectedHttpStatus(HttpStatusCode.NoContent)
                        .WithHelpDocsLink("core/tickets#delete-multiple-tickets-permanently")
                        .Build();
                }

                return await response.Content.ReadAsAsync<JobStatusResponse>();
            }
        }
    }
}
