using ZendeskApi.Contracts.Models;
using ZendeskApi.Contracts.Responses;

namespace ZendeskApi.Client.Resources
{
    public class TicketFieldResource : ZendeskResource<TicketField>, ITicketFieldResource
    {
        protected override string ResourceUri
        {
            get { return @"/api/v2/ticket_fields"; }
        }

        public TicketFieldResource(IZendeskClient client)
        {
            Client = client;
        }

        public IResponse<TicketField> Get(long id)
        {
            return Get<TicketFieldResponse>(id);
        }

        public IListResponse<TicketField> GetAll()
        {
            return GetAll<TicketFieldListResponse>();
        }
    }
}
