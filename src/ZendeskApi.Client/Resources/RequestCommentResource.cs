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

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            return GetAll<TicketCommentListResponse>(parentId);
        }
    }
}
