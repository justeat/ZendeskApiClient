using System.Collections.Generic;
using JE.Api.ClientBase;
using JustEat.ZendeskApi.Client.Formatters;
using JustEat.ZendeskApi.Contracts.Models;
using JustEat.ZendeskApi.Contracts.Requests;
using JustEat.ZendeskApi.Contracts.Responses;

namespace JustEat.ZendeskApi.Client.Resources
{
    public class TicketResource : ITicketResource
    {
        private const string TicketUri = @"/api/v2/tickets";
        private const string ShowMany = "/show_many";

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

        public ListResponse<Ticket> GetAll(List<int> ticketIds)
        {
            var requestUri = _client.BuildUri(string.Format("{0}/{1}", TicketUri, ShowMany), string.Format("ids={0}", ZendeskFormatter.ToCsv(ticketIds)));

            return _client.Get<TicketListResponse>(requestUri);
        }

        public TicketResponse Post(TicketRequest ticket)
        {
            var requestUri = _client.BuildUri(TicketUri);

            return _client.Post<TicketResponse>(requestUri, ticket);
        }
    }
}
