using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class CreateOrganizationOperation : ICreateOrganizationOperation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("organization_fields")]
        public Dictionary<object, object> CustomFields { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("domain_names")]
        public List<string> DomainNames { get; set; }

        [JsonIgnore]
        [JsonProperty("shared_tickets")]
        public bool SharedTickets { get; set; }

        [JsonIgnore]
        [JsonProperty("shared_comments")]
        public bool SharedComments { get; set; }

        [JsonIgnore]
        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        public CreateOrganizationOperation()
        { }

        public CreateOrganizationOperation(ICreateOrganizationOperation createOrganizationOperation)
        {
            Name = createOrganizationOperation.Name;
            Details = createOrganizationOperation.Details;
            Notes = createOrganizationOperation.Notes;
            CustomFields = createOrganizationOperation.CustomFields?.ToDictionary(x => x.Key, x => x.Value);
            Tags = createOrganizationOperation.Tags?.ToList();
            ExternalId = createOrganizationOperation.ExternalId;
            DomainNames = createOrganizationOperation.DomainNames?.ToList();
            SharedTickets = createOrganizationOperation.SharedTickets;
            SharedComments = createOrganizationOperation.SharedComments;
            GroupId = createOrganizationOperation.GroupId;
        }
    }
}