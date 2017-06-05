using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormResource : ZendeskResource<TicketForm>, ITicketFormResource
    {
        private const string ResourceUri = "/api/v2/ticket_forms";

        public TicketFormResource(ZendeskOptions options) : base(options) { }

        public async Task<IResponse<TicketForm>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketFormResponse>();
            }
        }

        public async Task<IListResponse<TicketForm>> GetAllAsync()
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketFormListResponse>();
            }
        }
    }
}
