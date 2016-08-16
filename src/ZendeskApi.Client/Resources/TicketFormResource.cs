using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormResource : ZendeskResource<TicketForm>, ITicketFormResource
    {
        protected override string ResourceUri => @"/api/v2/ticket_forms";

        public TicketFormResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<TicketForm> Get(long id)
        {
            return GetAsync(id).Result;
        }

        public async Task<IResponse<TicketForm>> GetAsync(long id)
        {
            return await GetAsync<TicketFormResponse>(id).ConfigureAwait(false);
        }

        public IListResponse<TicketForm> GetAll()
        {
            return GetAllAsync().Result;
        }

        public async Task<IListResponse<TicketForm>> GetAllAsync()
        {
            return await GetAllAsync<TicketFormListResponse>().ConfigureAwait(false);
        }
    }
}
