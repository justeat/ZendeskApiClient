using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationFieldsReorderRequest
    {
        [JsonProperty("organization_field_ids")]
        public long[] OrganizationFieldIds { get; set; }
    }
}