using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client
{
    public interface IZendeskClient
    {
        ITicketsResource Tickets { get; }
        ITicketCommentsResource TicketComments { get; }
        IOrganizationResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupsResource Groups { get; }
        IUsersResource Users { get; }
        IUserIdentityResource UserIdentities { get; }
        IAttachmentsResource Attachments { get; }
        ITicketFieldsResource TicketFields { get; }
        ITicketFormResource TicketForms { get; }
        IOrganizationMembershipsResource OrganizationMemberships { get; }
        IRequestsResource Requests { get; }
        ISatisfactionRatingsResource SatisfactionRatings { get; }
    }
}