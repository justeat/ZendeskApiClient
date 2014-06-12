using JustEat.ZendeskApi.Client.Resources;

namespace JustEat.ZendeskApi.Client
{
    public interface IZendeskClient
    {
        ITicketResource Ticket { get; }
        ISearchResource Search { get; }
        IGroupResource Group { get; }
        IGroupResource AssigableGroup { get; }
    }
}