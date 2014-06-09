using JE.Api.ClientBase;
using JustEat.ZendeskApi.Contracts.Responses;


namespace JustEat.ZendeskApi.Client.Resources
{
    public class TicketResource
    {
        private readonly IBaseClient _client;

        public TicketResource(IBaseClient client)
        {
            _client = client;
        }

        public TicketResponse Get(int restaurantId)
        {
            var requestUri = _client.BuildUri(@"/api/v2/search.json", string.Format("query=query=type:ticket&restaurantid={0}", restaurantId));

            return _client.Get<TicketResponse>(requestUri);
        }
    }
}
