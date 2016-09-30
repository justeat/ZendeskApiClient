using System.Threading.Tasks;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldResource : ZendeskResource<TicketField>, ITicketFieldResource
    {
        private const string ResourceUri = "/api/v2/ticket_fields";

        public TicketFieldResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<TicketField> Get(long id)
        {
            return Get<TicketFieldResponse>($"{ResourceUri}/{id}");
        }

        public async Task<IResponse<TicketField>> GetAsync(long id)
        {
            return await GetAsync<TicketFieldResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public IListResponse<TicketField> GetAll()
        {
            return GetAll<TicketFieldListResponse>(ResourceUri);
        }

        public async Task<IListResponse<TicketField>> GetAllAsync()
        {
            return await GetAllAsync<TicketFieldListResponse>(ResourceUri).ConfigureAwait(false);
        }
    }
}
