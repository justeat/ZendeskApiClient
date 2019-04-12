using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldsResource : ITicketFieldsResource
    {
        private const string ResourceUri = "api/v2/ticket_fields";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(TicketFieldsResource).Name + ": {0}");

        public TicketFieldsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<TicketField>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("ticket_fields#list-ticket-fields");

                return await response.Content.ReadAsAsync<TicketFieldsResponse>();
            }
        }

        public async Task<TicketField> GetAsync(long ticketFieldId)
        {
            using (_loggerScope(_logger, $"GetAsync({ticketFieldId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(ticketFieldId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("TicketResponse Field {0} not found", ticketFieldId);
                    return null;
                }

                await response.ThrowIfUnsuccessful("ticket_fields#show-ticket-field");

                var singleResponse = await response.Content.ReadAsAsync<TicketFieldResponse>();
                return singleResponse.TicketField;
            }
        }

        public async Task<TicketField> CreateAsync(TicketField ticketField)
        {
            using (_loggerScope(_logger, "PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var request = new TicketFieldCreateUpdateRequest(ticketField);
                var response = await client.PostAsJsonAsync(ResourceUri, request).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    await response.ThrowZendeskRequestException(
                        "ticket_fields#create-ticket-field",
                        System.Net.HttpStatusCode.Created);
                }

                var result = await response.Content.ReadAsAsync<TicketFieldResponse>();
                return result.TicketField;
            }
        }

        public async Task<TicketField> UpdateAsync(TicketField ticketField)
        {
            using (_loggerScope(_logger, "PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var request = new TicketFieldCreateUpdateRequest(ticketField);
                var response = await client.PutAsJsonAsync(ticketField.Id.ToString(), request).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update ticket field as ticket field {0} cannot be found", ticketField.Id);
                    return null;
                }

                await response.ThrowIfUnsuccessful("ticket_fields#update-ticket-field");

                var result = await response.Content.ReadAsAsync<TicketFieldResponse>();
                return result.TicketField;
            }
        }

        public async Task DeleteAsync(long ticketFieldId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({ticketFieldId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(ticketFieldId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    await response.ThrowZendeskRequestException(
                        "ticket_fields#delete-ticket-field",
                        System.Net.HttpStatusCode.NoContent);
                }
            }
        }
    }
}
