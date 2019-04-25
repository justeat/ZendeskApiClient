using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormsResource : AbstractBaseResource<TicketFormsResource>,
        ITicketFormsResource
    {
        private const string ResourceUri = "api/v2/ticket_forms";

        public TicketFormsResource(
            IZendeskApiClient apiClient,
            ILogger logger)
            : base(apiClient, logger, "ticket_forms")
        { }

        public async Task<IPagination<TicketForm>> GetAllAsync(PagerParameters pager = null)
        {
            return await GetAsync<TicketFormsResponse>(
                ResourceUri,
                "list-ticket-forms",
                "GetAllAsync",
                pager);
        }

        public async Task<TicketForm> GetAsync(long ticketformId)
        {
            var response = await GetWithNotFoundCheckAsync<TicketFormResponse>(
                $"{ResourceUri}/{ticketformId}",
                "show-ticket-form",
                $"GetAsync({ticketformId})",
                $"TicketResponse Form {ticketformId} not found");

            return response?
                .TicketForm;
        }

        public async Task<IPagination<TicketForm>> GetAllAsync(
            long[] ticketFormsIds, 
            PagerParameters pager = null)
        {
            var ids = ZendeskFormatter.ToCsv(ticketFormsIds);

            return await GetAsync<TicketFormsResponse>(
                $"{ResourceUri}/show_many?ids={ids}",
                "show-many-ticket-forms",
                $"GetAllAsync({ids})",
                pager);
        }

        public async Task<TicketForm> CreateAsync(TicketForm ticketForm)
        {
            var response = await CreateAsync<TicketFormResponse, TicketFormCreateUpdateRequest>(
                ResourceUri,
                new TicketFormCreateUpdateRequest(ticketForm),
                "create-ticket-forms");

            return response?
                .TicketForm;
        }

        public async Task<TicketForm> UpdateAsync(TicketForm ticketForm)
        {
            var response = await UpdateWithNotFoundCheckAsync<TicketFormResponse, TicketFormCreateUpdateRequest>(
                $"{ResourceUri}/{ticketForm.Id}",
                new TicketFormCreateUpdateRequest(ticketForm),
                "update-ticket-forms",
                $"Cannot update ticket form as ticket form {ticketForm.Id} cannot be found");

            return response?
                .TicketForm;
        }

        public async Task DeleteAsync(long ticketFormId)
        {
            await DeleteAsync(
                ResourceUri,
                ticketFormId,
                "delete-ticket-form");
        }
    }
}
