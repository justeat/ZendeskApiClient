using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [SearchResultType("user")]
    public class UserResponse : ISearchResult
    {
        /// <summary>
        /// Automatically assigned when the user is created
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The user's primary email address
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// The user's name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// false if the user has been deleted
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// An alias displayed to end users
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Whether or not the user is a chat-only agent
        /// </summary>
        [JsonProperty("chat_only")]
        public bool ChatOnly { get; set; }

        /// <summary>
        /// The time the user was created
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// A custom role if the user is an agent on the Enterprise plan
        /// </summary>
        [JsonProperty("custom_role_id")]
        public long? CustomRoleId { get; set; }

        /// <summary>
        /// Any details you want to store about the user, such as an address
        /// </summary>
        [JsonProperty("details")]
        public string Details { get; set; }

        /// <summary>
        /// A unique id you can specify for the user
        /// </summary>
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        /// <summary>
        /// The last time the user signed in to Zendesk Support
        /// </summary>
        [JsonProperty("last_login_at")]
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// The user's locale
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// The user's language identifier
        /// </summary>
        [JsonProperty("locale_id")]
        public long? LocaleId { get; set; }

        /// <summary>
        /// Designates whether the user has forum moderation capabilities
        /// </summary>
        [JsonProperty("moderator")]
        public bool Moderator { get; set; }

        /// <summary>
        /// Any notes you want to store about the user
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// true if the user can only create private comments
        /// </summary>
        [JsonProperty("only_private_comments")]
        public bool OnlyPrivateComments { get; set; }

        /// <summary>
        /// The id of the organization the user is associated with
        /// </summary>
        [JsonProperty("organization_id")]
        public long? OrganizationId { get; set; }

        /// <summary>
        /// The id of the user's default group. *Can only be set on create, not on update
        /// </summary>
        [JsonProperty("default_group_id")]
        public long? DefaultGroupId { get; set; }

        /// <summary>
        /// The user's primary phone number. See <see href="https://developer.zendesk.com/rest_api/docs/core/users#phone-number">Phone Number</see> below
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        //TODO: Support attachments (photo)

        /// <summary>
        /// If the agent has any restrictions; false for admins and unrestricted agents, true for other agents
        /// </summary>
        [JsonProperty("restricted_agent")]
        public bool RestrictedAgent { get; set; }

        /// <summary>
        /// The user's role. Possible values are "end-user", "agent", or "admin"
        /// </summary>
        [JsonProperty("role")]
        public UserRole Role { get; set; }

        /// <summary>
        /// If the user is shared from a different Zendesk Support instance. Ticket sharing accounts only
        /// </summary>
        [JsonProperty("shared")]
        public bool Shared { get; set; }

        /// <summary>
        /// If the user is a shared agent from a different Zendesk Support instance. Ticket sharing accounts only
        /// </summary>
        [JsonProperty("shared_agent")]
        public bool SharedAgent { get; set; }

        /// <summary>
        /// The user's signature. Only agents and admins can have signatures
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// If the agent is suspended. Tickets from suspended users are also suspended, and these users cannot sign in to the end user portal
        /// </summary>
        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        /// <summary>
        /// The user's tags. Only present if your account has user tagging enabled
        /// </summary>
        [JsonProperty("tags")]
        public IReadOnlyList<string> Tags { get; set; }

        /// <summary>
        /// Specifies which tickets the user has access to. Possible values are: "organization", "groups", "assigned", "requested", null
        /// </summary>
        [JsonProperty("ticket_restriction")]
        public TicketRestriction? TicketRestriction { get; set; }

        /// <summary>
        /// The user's time zone
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// If two factor authentication is enabled.
        /// </summary>
        [JsonProperty("two_factor_auth_enabled")]
        public bool TwoFactorAuthEnabled { get; set; }

        /// <summary>
        /// The time the user was last updated
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// The user's API url
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        //TODO: Is object correct??
        /// <summary>
        /// Values of custom fields in the user's profile
        /// </summary>
        [JsonProperty("user_fields")]
        public IReadOnlyDictionary<string, object> UserFields { get; set; }

        /// <summary>
        /// If the user's identity has been verified or not
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; set; }


        [JsonProperty("result_type")]
        string ResultType => "user";
    }
}
