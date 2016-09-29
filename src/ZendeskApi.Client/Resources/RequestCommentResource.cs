using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestCommentResource : IRequestCommentResource
    {
        private readonly IRestClient _client;

        public RequestCommentResource(IRestClient client)
        {
            _client = client;
        }

        public IResponse<TicketComment> Get(long id, long parentId)
        {
            var requestUri = _client.BuildUri($"/api/v2/requests/{parentId}/comments/{id}");
            return _client.Get<TicketCommentResponse>(requestUri);
        }

        public async Task<IResponse<TicketComment>> GetAsync(long id, long parentId)
        {
            var requestUri = _client.BuildUri($"/api/v2/requests/{parentId}/comments/{id}");
            return await _client.GetAsync<TicketCommentResponse>(requestUri).ConfigureAwait(false);
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            var requestUri = _client.BuildUri($"/api/v2/requests/{parentId}/comments");
            return _client.Get<TicketCommentListResponse>(requestUri);
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            var requestUri = _client.BuildUri($"/api/v2/requests/{parentId}/comments");
            return await _client.GetAsync<TicketCommentListResponse>(requestUri).ConfigureAwait(false);
        }
    }
}
