using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketCommentResource : ITicketCommentResource
    {
        private readonly IRestClient _client;

        public TicketCommentResource(IRestClient client)
        {
            _client = client;
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            var requestUri = _client.BuildUri($"/api/v2/tickets/{parentId}/comments");
            return _client.Get<TicketCommentListResponse>(requestUri);
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            var requestUri = _client.BuildUri($"/api/v2/tickets/{parentId}/comments");
            return await _client.GetAsync<TicketCommentListResponse>(requestUri).ConfigureAwait(false);
        }
    }
}
