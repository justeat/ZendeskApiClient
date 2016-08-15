using System.Threading.Tasks;
using ZendeskApi.Client.Http;
using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class RequestCommentResource : ZendeskResource<TicketComment>, IRequestCommentResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/requests/{0}/comments"; }
        }

        public RequestCommentResource(IRestClient client)
        {
            Client = client;
        }

        public IResponse<TicketComment> Get(long id, long parentId)
        {
            return Get<TicketCommentResponse>(id, parentId);
        }

        public async Task<IResponse<TicketComment>> GetAsync(long id, long parentId)
        {
            return await GetAsync<TicketCommentResponse>(id, parentId);
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            return GetAll<TicketCommentListResponse>(parentId);
        }

        public async Task<IListResponse<TicketComment>> GetAllAsync(long parentId)
        {
            return await GetAllAsync<TicketCommentListResponse>(parentId);
        }
    }
}
