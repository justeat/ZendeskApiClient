using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationFieldCreateUpdateRequest
    {
        public OrganizationFieldCreateUpdateRequest(OrganizationField organizationField)
        {
            OrganizationField = organizationField;
        }
        
        [JsonProperty("organization_field")]
        public OrganizationField OrganizationField { get; set; }
    }
}