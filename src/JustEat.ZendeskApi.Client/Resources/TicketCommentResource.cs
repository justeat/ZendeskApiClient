using JustEat.ZendeskApi.Client.Http;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class TicketCommentResource : ZendeskResource<TicketComment>, ITicketCommentResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/tickets/{0}/comments"; }
        }

        public TicketCommentResource(IRestClient client)
        {
            Client = client;
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            return GetAll<TicketCommentListResponse>(parentId);
        }
    }
}
