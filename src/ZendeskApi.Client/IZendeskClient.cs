using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client
{
    public interface IZendeskClient
    {
        IArticlesResources Articles { get; }
        ITicketsResource Tickets { get; }
        ITicketCommentsResource TicketComments { get; }
        IDeletedTicketsResource DeletedTickets { get; }
        IOrganizationsResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupsResource Groups { get; }
        IUsersResource Users { get; }
        IDeletedUsersResource DeletedUsers { get; }
        IUserIdentityResource UserIdentities { get; }
        IAttachmentsResource Attachments { get; }
        ITicketFieldsResource TicketFields { get; }
        ITicketFormsResource TicketForms { get; }
        IOrganizationMembershipsResource OrganizationMemberships { get; }
        IRequestsResource Requests { get; }
        ISatisfactionRatingsResource SatisfactionRatings { get; }
        IUserFieldsResource UserFields { get; }
        IJobStatusResource JobStatuses { get; }
    }
}