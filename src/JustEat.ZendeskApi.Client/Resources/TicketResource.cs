using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class TicketResource
    {
        private const string TicketUri = @"/api/v2/tickets";

        private readonly IBaseClient _client;

        public TicketResource(IBaseClient client)
        {
            _client = client;
        }

        public TicketResponse Get(int ticketId)
        {
            var requestUri = _client.BuildUri(string.Format("{0}/{1}", TicketUri, ticketId));

            return _client.Get<TicketResponse>(requestUri);
        }

        public TicketResponse Post(TicketRequest ticket)
        {
            var requestUri = _client.BuildUri(TicketUri);

            return _client.Post<TicketResponse>(requestUri, ticket);
        }
    }
}
