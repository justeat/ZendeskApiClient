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
        
        public ZendeskClient(IZendeskApiClient apiClient)
        {
            Tickets = new TicketResource(apiClient);
            TicketComments = new TicketCommentResource(apiClient);
            RequestComments = new RequestCommentResource(apiClient);
            Organizations = new OrganizationResource(apiClient);
            Search = new SearchResource(apiClient);
            Groups = new GroupsResource(apiClient);
            AssignableGroups = new AssignableGroupResource(apiClient);
            Users = new UserResource(apiClient);
            UserIdentities = new UserIdentityResource(apiClient);
            Upload = new UploadResource(apiClient);
            TicketFields = new TicketFieldResource(apiClient);
            TicketForms = new TicketFormResource(apiClient);
            OrganizationMemberships = new OrganizationMembershipResource(apiClient);
            Request = new RequestResource(apiClient);
            SatisfactionRating = new SatisfactionRatingResource(apiClient);
        }
    }
}
