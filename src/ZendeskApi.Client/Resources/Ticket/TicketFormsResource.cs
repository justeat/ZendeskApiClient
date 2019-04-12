using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormsResource : ITicketFormsResource
    {
        private const string ResourceUri = "api/v2/ticket_forms";

        private readonly IZendeskApiClient _apiClient;
        private readonly ILogger _logger;

        private Func<ILogger, string, IDisposable> _loggerScope =
            LoggerMessage.DefineScope<string>(typeof(TicketFormsResource).Name + ": {0}");

        public TicketFormsResource(IZendeskApiClient apiClient,
            ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<IPagination<TicketForm>> GetAllAsync(PagerParameters pager = null)
        {
            using (_loggerScope(_logger, "GetAllAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("ticket_forms#list-ticket-forms");

                return await response.Content.ReadAsAsync<TicketFormsResponse>();
            }
        }

        public async Task<TicketForm> GetAsync(long ticketformId)
        {
            using (_loggerScope(_logger, $"GetAsync({ticketformId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync(ticketformId.ToString()).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("TicketResponse Form {0} not found", ticketformId);
                    return null;
                }

                await response.ThrowIfUnsuccessful("ticket_forms#show-ticket-form");

                var singleResponse = await response.Content.ReadAsAsync<TicketFormResponse>();
                return singleResponse.TicketForm;
            }
        }

        public async Task<IPagination<TicketForm>> GetAllAsync(long[] ticketFormsIds, PagerParameters pager = null)
        {
            using (_loggerScope(_logger, $"GetAllAsync({ZendeskFormatter.ToCsv(ticketFormsIds)})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.GetAsync($"show_many?ids={ZendeskFormatter.ToCsv(ticketFormsIds)}", pager).ConfigureAwait(false);

                await response.ThrowIfUnsuccessful("ticket_forms#show-many-ticket-forms");

                return await response.Content.ReadAsAsync<TicketFormsResponse>();
            }
        }

        public async Task<TicketForm> CreateAsync(TicketForm ticketForm)
        {
            using (_loggerScope(_logger, $"PostAsync"))
            using (var client = _apiClient.CreateClient())
            {
                var response = await client.PostAsJsonAsync(ResourceUri, ticketForm).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.Created)
                {
                    await response.ThrowZendeskRequestException(
                        "ticket_forms#create-ticket-forms",
                        System.Net.HttpStatusCode.Created);
                }

                return (await response.Content.ReadAsAsync<TicketForm>());
            }
        }

        public async Task<TicketForm> UpdateAsync(TicketForm ticketForm)
        {
            using (_loggerScope(_logger, $"PutAsync"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.PutAsJsonAsync(ticketForm.Id.ToString(), ticketForm).ConfigureAwait(false);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("Cannot update ticket form as ticket form {0} cannot be found", ticketForm.Id);
                    return null;
                }

                await response.ThrowIfUnsuccessful("ticket_forms#update-ticket-forms");

                return await response.Content.ReadAsAsync<TicketForm>();
            }
        }

        public async Task DeleteAsync(long ticketFormId)
        {
            using (_loggerScope(_logger, $"DeleteAsync({ticketFormId})"))
            using (var client = _apiClient.CreateClient(ResourceUri))
            {
                var response = await client.DeleteAsync(ticketFormId.ToString()).ConfigureAwait(false);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    await response.ThrowZendeskRequestException(
                        "ticket_forms#delete-ticket-form",
                        System.Net.HttpStatusCode.NoContent);
                }
            }
        }
    }
}
