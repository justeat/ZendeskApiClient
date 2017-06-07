using Microsoft.Extensions.Logging;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client
{
    public class ZendeskClient : IZendeskClient
    {
        public ITicketsResource Tickets { get; private set; }
        public ITicketCommentResource TicketComments { get; private set; }
        public IRequestCommentResource RequestComments { get; private set; }
        public IOrganizationResource Organizations { get; private set; }
        public ISearchResource Search { get; private set; }
        public IGroupsResource Groups { get; private set; }
        public IUserResource Users { get; private set; }
        public IUserIdentityResource UserIdentities { get; private set; }
        public IAttachmentsResource Attachments { get; private set; }
        public ITicketFieldResource TicketFields { get; private set; }
        public ITicketFormResource TicketForms { get; private set; }
        public IOrganizationMembershipsResource OrganizationMemberships { get; private set; }
        public IRequestResource Request { get; private set; }
        public ISatisfactionRatingResource SatisfactionRating { get; private set; }
        
        public ZendeskClient(IZendeskApiClient apiClient, ILogger logger)
        {
            Tickets = new TicketsResource(apiClient, logger);
            TicketComments = new TicketCommentResource(apiClient);
            RequestComments = new RequestCommentResource(apiClient);
            Organizations = new OrganizationResource(apiClient);
            Search = new SearchResource(apiClient);
            Groups = new GroupsResource(apiClient, logger);
            Users = new UserResource(apiClient);
            UserIdentities = new UserIdentityResource(apiClient);
            Attachments = new AttachmentsResource(apiClient, logger);
            TicketFields = new TicketFieldResource(apiClient);
            TicketForms = new TicketFormResource(apiClient);
            OrganizationMemberships = new OrganizationMembershipsResource(apiClient, logger);
            Request = new RequestResource(apiClient);
            SatisfactionRating = new SatisfactionRatingResource(apiClient);
        }
    }
}
