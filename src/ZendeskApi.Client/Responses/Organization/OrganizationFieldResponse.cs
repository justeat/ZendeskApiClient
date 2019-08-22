using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationFieldResponse
    {
        [JsonProperty("organization_field")]
        public OrganizationField OrganizationField { get; set; }
    }
}