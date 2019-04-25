using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldsResource : AbstractBaseResource<TicketFieldsResource>,
        ITicketFieldsResource
    {
        private const string ResourceUri = "api/v2/ticket_fields";

        public TicketFieldsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "ticket_fields")
        { }

        public async Task<IPagination<TicketField>> GetAllAsync(PagerParameters pager = null)
        {
            return await GetAsync<TicketFieldsResponse>(
                ResourceUri,
                "list-ticket-fields",
                "GetAllAsync",
                pager);
        }

        public async Task<TicketField> GetAsync(long ticketFieldId)
        {
            var response = await GetWithNotFoundCheckAsync<TicketFieldResponse>(
                $"{ResourceUri}/{ticketFieldId}",
                "show-ticket-field",
                $"GetAsync({ticketFieldId})",
                $"TicketResponse Field {ticketFieldId} not found");

            return response?
                .TicketField;
        }

        public async Task<TicketField> CreateAsync(TicketField ticketField)
        {
            var response = await CreateAsync<TicketFieldResponse, TicketFieldCreateUpdateRequest>(
                ResourceUri,
                new TicketFieldCreateUpdateRequest(ticketField),
                "create-ticket-field");

            return response?
                .TicketField;
        }

        public async Task<TicketField> UpdateAsync(TicketField ticketField)
        {
            var response = await UpdateWithNotFoundCheckAsync<TicketFieldResponse, TicketFieldCreateUpdateRequest>(
                $"{ResourceUri}/{ticketField.Id}",
                new TicketFieldCreateUpdateRequest(ticketField),
                "update-ticket-field",
                $"Cannot update ticket field as ticket field {ticketField.Id} cannot be found");

            return response?
                .TicketField;
        }

        public async Task DeleteAsync(long ticketFieldId)
        {
            await DeleteAsync(
                ResourceUri,
                ticketFieldId,
                "delete-ticket-field");
        }
    }
}
