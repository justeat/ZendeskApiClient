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
        IGroupResource Groups { get; }
        IAssignableGroupResource AssignableGroups { get; }
        IUserResource Users { get; }
        IUserIdentityResource UserIdentities { get; }
        IUploadResource Upload { get; }
        IOrganizationMembershipResource OrganizationMemberships { get; }
        IRequestResource Requests { get; }
    }
}