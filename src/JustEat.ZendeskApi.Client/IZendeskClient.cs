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
        IGroupResource AssigableGroups { get; }
        IUserResource Users { get; }
    }
}