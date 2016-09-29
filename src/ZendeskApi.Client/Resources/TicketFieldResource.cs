using System.Threading.Tasks;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldResource : ITicketFieldResource
    {
        private readonly IZendeskClient _client;
        private const string ResourceUri = "/api/v2/ticket_fields";

        public TicketFieldResource(IZendeskClient client)
        {
            _client = client;
        }

        public IResponse<TicketField> Get(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return _client.Get<TicketFieldResponse>(requestUri);
        }

        public async Task<IResponse<TicketField>> GetAsync(long id)
        {
            var requestUri = _client.BuildUri($"{ResourceUri}/{id}");
            return await _client.GetAsync<TicketFieldResponse>(requestUri).ConfigureAwait(false);
        }

        public IListResponse<TicketField> GetAll()
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return _client.Get<TicketFieldListResponse>(requestUri);
        }

        public async Task<IListResponse<TicketField>> GetAllAsync()
        {
            var requestUri = _client.BuildUri(ResourceUri);
            return await _client.GetAsync<TicketFieldListResponse>(requestUri).ConfigureAwait(false);
        }
    }
}
