using JustEat.ZendeskApi.Client.Resources;

namespace JustEat.ZendeskApi.Client
{
    public interface IZendeskClient
    {
        ITicketResource Tickets { get; }
        ITicketCommentResource TicketComments { get; }
        IOrganizationResource Organizations { get; }
        ISearchResource Search { get; }
        IGroupResource Groups { get; }
        IAssignableGroupResource AssignableGroups { get; }
        IUserResource Users { get; }
    }
}