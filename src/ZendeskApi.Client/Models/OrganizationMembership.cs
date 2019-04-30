using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("organization_membership")]
    public class OrganizationMembership : IZendeskEntity
    {
        [JsonProperty]
        public long Id { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("organization_id")]
        public long OrganizationId { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
