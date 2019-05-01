using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/support/organizations#update-organization
    /// </summary>
    internal class OrganizationUpdateRequest
    {
        public OrganizationUpdateRequest(UpdateOrganizationOperation organization)
        {
            Organization = organization;
        }

        /// <summary>
        /// The Organization to update
        /// </summary>
        [JsonProperty("organization")]
        public UpdateOrganizationOperation Organization { get; set; }
    }
}