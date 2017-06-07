using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client
{
    public interface IZendeskClient
    {
        ITicketResource Tickets { get; }
        ITicketCommentResource TicketComments { get; }
        IRequestCommentResource RequestComments { get; }
        IOrganizationResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupsResource Groups { get; }
        IUserResource Users { get; }
        IUserIdentityResource UserIdentities { get; }
        IUploadResource Upload { get; }
        ITicketFieldResource TicketFields { get; }
        ITicketFormResource TicketForms { get; }
        IOrganizationMembershipResource OrganizationMemberships { get; }
        IRequestResource Request { get; }
        ISatisfactionRatingResource SatisfactionRating { get; }
    }
}