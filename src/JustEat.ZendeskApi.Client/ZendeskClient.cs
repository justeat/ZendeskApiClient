using System;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Http;
using JE.Api.ClientBase.Serialization;
using JustEat.ZendeskApi.Client.Resources;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskClient: BaseClient
    {
        public ITicketResource Ticket { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Group { get; private set; }
        public IGroupResource AssigableGroup { get; private set; }

        public ZendeskClient(Uri baseUri, ZendeskDefaultConfiguration configuration, IHttpChannel httpChannel = null, ISerializer serializer = null, ILogAdapter logger = null)
            : base(baseUri, configuration, httpChannel, serializer, logger)
        {
            Ticket = new TicketResource(this);
            Search = new SearchResource(this);
            Group = new GroupsResource(this);
            AssigableGroup = new AssignableGroupResource(this);
        }
    }
}
