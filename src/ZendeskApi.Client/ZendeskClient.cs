using Microsoft.Extensions.Options;
using ZendeskApi.Client.Options;
using ZendeskApi.Client.Resources;


namespace ZendeskApi.Client
{
    public class ZendeskClient : IZendeskClient
    {
        public ITicketResource Tickets { get; private set; }
        public ITicketCommentResource TicketComments { get; private set; }
        public IRequestCommentResource RequestComments { get; private set; }
        public IOrganizationResource Organizations { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupResource Groups { get; private set; }
        public IAssignableGroupResource AssignableGroups { get; private set; }
        public IUserResource Users { get; private set; }
        public IUserIdentityResource UserIdentities { get; private set; }
        public IUploadResource Upload { get; private set; }
        public ITicketFieldResource TicketFields { get; private set; }
        public ITicketFormResource TicketForms { get; private set; }
        public IOrganizationMembershipResource OrganizationMemberships { get; private set; }
        public IRequestResource Request { get; private set; }
        public ISatisfactionRatingResource SatisfactionRating { get; private set; }
        
        public ZendeskClient(IOptions<ZendeskOptions> optionsAccessor)
        {
            var options = optionsAccessor.Value;

            Tickets = new TicketResource(options);
            TicketComments = new TicketCommentResource(options);
            RequestComments = new RequestCommentResource(options);
            Organizations = new OrganizationResource(options);
            Search = new SearchResource(options);
            Groups = new GroupsResource(options);
            AssignableGroups = new AssignableGroupResource(options);
            Users = new UserResource(options);
            UserIdentities = new UserIdentityResource(options);
            Upload = new UploadResource(options);
            TicketFields = new TicketFieldResource(options);
            TicketForms = new TicketFormResource(options);
            OrganizationMemberships = new OrganizationMembershipResource(options);
            Request = new RequestResource(options);
            SatisfactionRating = new SatisfactionRatingResource(options);
        }
    }
}
