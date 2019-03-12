using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/support/organizations#update-organization
    /// </summary>
    public class OrganizationUpdateRequest
    {
        public OrganizationUpdateRequest(Organization organization)
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