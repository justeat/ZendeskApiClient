using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("user")]
    public class User : ISearchResult
    {
        [JsonProperty()]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("locale_id")]
        public int? LocalId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("shared")]
        public bool Shared { get; set; }

        [JsonProperty("shared_agent")]
        public bool SharedAgent { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("custom_role_id")]
        public long? CustomRoleId { get; set; }

        [JsonProperty("moderator")]
        public bool Moderator { get; set; }

        [JsonProperty("only_private_comments")]
        public bool OnlyPrivateComments { get; set; }

        [JsonProperty("restricted_agent")]
        public bool RestrictedAgent { get; set; }

        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        [JsonProperty("ticket_restriction")]
        public string TicketRestriction { get; set; }

        [JsonProperty("user_fields")]
        public Dictionary<string, object> UserFields { get; set; }

        [JsonProperty("default_group_id")]
        public long? DefaultGroupId { get; set; }

        DateTime ISearchResult.CreatedAt => CreatedAt.Value;
        DateTime ISearchResult.UpdatedAt => UpdatedAt.Value;
        long ISearchResult.Id => Id.Value;
        Uri ISearchResult.Url => Url;

        [JsonProperty("result_type")]
        string ISearchResult.Type => typeof(User).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}
