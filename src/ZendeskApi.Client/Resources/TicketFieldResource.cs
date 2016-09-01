using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldResource : ZendeskResource<TicketField>, ITicketFieldResource
    {
        protected override string ResourceUri => @"/api/v2/ticket_fields";

        public TicketFieldResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<TicketField> Get(long id)
        {
            return GetAsync(id).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IResponse<TicketField>> GetAsync(long id)
        {
            return await GetAsync<TicketFieldResponse>(id).ConfigureAwait(false);
        }

        public IListResponse<TicketField> GetAll()
        {
            return GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IListResponse<TicketField>> GetAllAsync()
        {
            return await GetAllAsync<TicketFieldListResponse>().ConfigureAwait(false);
        }
    }
}
