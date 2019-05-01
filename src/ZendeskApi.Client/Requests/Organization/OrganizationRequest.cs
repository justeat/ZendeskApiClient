using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class OrganizationRequest<T>
    {
        public OrganizationRequest(T organization)
        {
            Organization = organization;
        }

        /// <summary>
        /// The Organization to create
        /// </summary>
        [JsonProperty("organization")]
        public T Organization { get; set; }
    }
}