using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFormResource : ITicketFormResource
    {
        private readonly IZendeskClient _client;
        private const string ResourceUri = "/api/v2/ticket_forms";

        public TicketFormResource(IZendeskClient client)
        {
            _client = client;
        }

        public IResponse<TicketForm> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<TicketFormResponse>(requestUri);
        }

        public async Task<IResponse<TicketForm>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<TicketFormResponse>(requestUri);
        }

        public IListResponse<TicketForm> GetAll()
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return _client.Get<TicketFormListResponse>(requestUri);
        }

        public async Task<IListResponse<TicketForm>> GetAllAsync()
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return await _client.GetAsync<TicketFormListResponse>(requestUri).ConfigureAwait(false);
        }
    }
}
