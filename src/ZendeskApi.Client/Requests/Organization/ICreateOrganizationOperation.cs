using System.Collections.Generic;

namespace ZendeskApi.Client.Requests
{
    public interface ICreateOrganizationOperation
    {
        string Name { get; set; }
        string Details { get; set; }
        string Notes { get; set; }
        Dictionary<object, object> CustomFields { get; set; }
        List<string> Tags { get; set; }
        string ExternalId { get; set; }
        List<string> DomainNames { get; set; }
        bool SharedTickets { get; set; }
        bool SharedComments { get; set; }
        long GroupId { get; set; }
    }
}
