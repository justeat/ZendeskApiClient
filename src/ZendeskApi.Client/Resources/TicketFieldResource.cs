using System.Threading.Tasks;
using ZendeskApi.Client.Options;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldResource : ZendeskResource<TicketField>, ITicketFieldResource
    {
        private const string ResourceUri = "/api/v2/ticket_fields";

        public TicketFieldResource(ZendeskOptions options) : base(options) { }

        public async Task<IResponse<TicketField>> GetAsync(long id)
        {
            using (var client = CreateZendeskClient(ResourceUri + "/"))
            {
                var response = await client.GetAsync(id.ToString()).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketFieldResponse>();
            }
        }

        public async Task<IListResponse<TicketField>> GetAllAsync()
        {
            using (var client = CreateZendeskClient("/"))
            {
                var response = await client.GetAsync(ResourceUri).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TicketFieldListResponse>();
            }
        }
    }
}
