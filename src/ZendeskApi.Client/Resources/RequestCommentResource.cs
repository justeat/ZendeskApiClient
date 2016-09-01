using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestCommentResource : ZendeskResource<TicketComment>, IRequestCommentResource
    {
        protected override string ResourceUri => @"/api/v2/requests/{0}/comments";

        public RequestCommentResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<TicketComment> Get(long id, long parentId)
        {
            return GetAsync(id, parentId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IResponse<TicketComment>> GetAsync(long id, long parentId)
        {
            return await GetAsync<TicketCommentResponse>(id, parentId).ConfigureAwait(false);
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            return GetAllAsync(parentId).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            return await GetAllAsync<TicketCommentListResponse>(parentId).ConfigureAwait(false);
        }
    }
}
