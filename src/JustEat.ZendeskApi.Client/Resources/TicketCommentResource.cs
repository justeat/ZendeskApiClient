using JE.Api.ClientBase;
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

        public TicketCommentResource(IZendeskClient client)
        {
            Client = client;
        }

        public IListResponse<TicketComment> GetAll(long parentId)
        {
            return GetAll<TicketCommentListResponse>(parentId);
        }
    }
}
