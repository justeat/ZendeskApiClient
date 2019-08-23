using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Resources.Interfaces;

namespace ZendeskApi.Client
{
    public interface IZendeskClient
    {
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
        ITicketAuditResource TicketAudits { get; }
        IOrganizationFieldsResource OrganizationFields { get; }
        IOrganizationMembershipsResource OrganizationMemberships { get; }
        IRequestsResource Requests { get; }
        ISatisfactionRatingsResource SatisfactionRatings { get; }
        IUserFieldsResource UserFields { get; }
        IJobStatusResource JobStatuses { get; }
        IServiceStatusResource ServiceStatus { get; }
    }
}