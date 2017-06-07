using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client
{
    public interface IZendeskClient
    {
        ITicketsResource Tickets { get; }
        ITicketCommentResource TicketComments { get; }
        IRequestCommentResource RequestComments { get; }
        IOrganizationResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupsResource Groups { get; }
        IUserResource Users { get; }
        IUserIdentityResource UserIdentities { get; }
        IAttachmentsResource Attachments { get; }
        ITicketFieldResource TicketFields { get; }
        ITicketFormResource TicketForms { get; }
        IOrganizationMembershipsResource OrganizationMemberships { get; }
        IRequestResource Request { get; }
        ISatisfactionRatingResource SatisfactionRating { get; }
    }
}