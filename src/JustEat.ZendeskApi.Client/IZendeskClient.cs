using JustEat.ZendeskApi.Client.Resources;

namespace JustEat.ZendeskApi.Client
{
    public interface IZendeskClient
    {
        ITicketResource Ticket { get; }
        IOrganizationResource Organization { get; }
        ISearchResource Search { get; }
        IGroupResource Group { get; }
        IGroupResource AssigableGroup { get; }
    }
}