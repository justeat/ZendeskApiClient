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
            return Get<TicketFormResponse>(id);
        }

        public async Task<IResponse<TicketForm>> GetAsync(long id)
        {
            return await GetAsync<TicketFormResponse>(id);
        }

        public IListResponse<TicketForm> GetAll()
        {
            return GetAll<TicketFormListResponse>();
        }

        public async Task<IListResponse<TicketForm>> GetAllAsync()
        {
            return await GetAllAsync<TicketFormListResponse>();
        }
    }
}
