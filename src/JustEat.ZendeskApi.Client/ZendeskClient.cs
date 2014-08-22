using System;
using JustEat.ZendeskApi.Client.Http;
using JustEat.ZendeskApi.Client.Resources;
using ILogAdapter = JustEat.ZendeskApi.Client.Logging.ILogAdapter;
using ISerializer = JustEat.ZendeskApi.Client.Serialization.ISerializer;


namespace JustEat.ZendeskApi.Client
{
    public class ZendeskClient : ClientBase, IZendeskClient
    {
        public ITicketResource Tickets { get; private set; }
        public ITicketCommentResource TicketComments { get; private set; }
        public IOrganizationResource Organizations { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Groups { get; private set; }
        public IAssignableGroupResource AssignableGroups { get; private set; }
        public IUserResource Users { get; private set; }
        public IUserIdentityResource UserIdentities { get; private set; }
        public IOrganizationMembershipResource OrganizationMemberships { get; private set; }

        public ZendeskClient(Uri baseUri, ZendeskDefaultConfiguration configuration, ISerializer serializer = null, IHttpChannel httpChannel = null, ILogAdapter logger = null)
            :base(baseUri, configuration, serializer, httpChannel, logger)
        {
            Tickets = new TicketResource(this);
            TicketComments = new TicketCommentResource(this);
            Organizations = new OrganizationResource(this);
            Search = new SearchResource(this);
            Groups = new GroupsResource(this);
            AssignableGroups = new AssignableGroupResource(this);
            Users = new UserResource(this);
            UserIdentities = new UserIdentityResource(this);
            OrganizationMemberships = new OrganizationMembershipResource(this);
        }
    }
}
