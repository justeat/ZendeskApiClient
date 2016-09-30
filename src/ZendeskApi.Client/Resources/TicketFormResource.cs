using System.Threading.Tasks;
using ZendeskApi.Client.Resources.ZendeskApi.Client.Resources;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormResource : ZendeskResource<TicketForm>, ITicketFormResource
    {
        private const string ResourceUri = "/api/v2/ticket_forms";

        public TicketFormResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<TicketForm> Get(long id)
        {
            return Get<TicketFormResponse>($"{ResourceUri}/{id}");
        }

        public async Task<IResponse<TicketForm>> GetAsync(long id)
        {
            return await GetAsync<TicketFormResponse>($"{ResourceUri}/{id}").ConfigureAwait(false);
        }

        public IListResponse<TicketForm> GetAll()
        {
            return GetAll<TicketFormListResponse>(ResourceUri);
        }

        public async Task<IListResponse<TicketForm>> GetAllAsync()
        {
            return await GetAllAsync<TicketFormListResponse>(ResourceUri).ConfigureAwait(false);
        }
    }
}
