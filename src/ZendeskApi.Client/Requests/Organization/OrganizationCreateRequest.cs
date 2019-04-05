using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/core/organizations#create-organization
    /// </summary>
    public class OrganizationCreateRequest
    {
        public OrganizationCreateRequest(Organization organization)
        {
            Organization = organization;
        }
        
        /// <summary>
        /// The Organization to create
        /// </summary>
        [JsonProperty("organization")]
        public Organization Organization { get; set; }
    }
}
