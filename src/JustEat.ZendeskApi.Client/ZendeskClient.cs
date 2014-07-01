using System;
using JE.Api.ClientBase;
using JE.Api.ClientBase.Http;
using JustEat.ZendeskApi.Client.Resources;

namespace JustEat.ZendeskApi.Client
{
    public class ZendeskClient: BaseClient, IZendeskClient
    {
        public ITicketResource Tickets { get; private set; }
        public ITicketCommentResource TicketComments { get; private set; }
        public IOrganizationResource Organizations { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Groups { get; private set; }
        public IAssignableGroupResource AssigableGroups { get; private set; }

        public ZendeskClient(Uri baseUri, ZendeskDefaultConfiguration configuration, IHttpChannel httpChannel = null, ILogAdapter logger = null)
            : base(baseUri, configuration, httpChannel, new ZendeskJsonSerializer(), logger)
        {
            Tickets = new TicketResource(this);
            TicketComments = new TicketCommentResource(this);
            Organizations = new OrganizationResource(this);
            Search = new SearchResource(this);
            Groups = new GroupsResource(this);
            AssigableGroups = new AssignableGroupResource(this);
        }
    }
}
