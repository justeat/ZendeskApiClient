using System;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Http;
using JustEat.ZendeskApi.Client.Resources;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskClient: BaseClient, IZendeskClient
    {
        public ITicketResource Ticket { get; private set; }
        public IOrganizationResource Organization { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Group { get; private set; }
        public IGroupResource AssigableGroup { get; private set; }

        public ZendeskClient(Uri baseUri, ZendeskDefaultConfiguration configuration, IHttpChannel httpChannel = null, ILogAdapter logger = null)
            : base(baseUri, configuration, httpChannel, new ZendeskJsonSerializer(), logger)
        {
            Ticket = new TicketResource(this);
            Organization = new OrganizationResource(this);
            Search = new SearchResource(this);
            Group = new GroupsResource(this);
            AssigableGroup = new AssignableGroupResource(this);
        }
    }
}
