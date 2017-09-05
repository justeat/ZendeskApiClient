using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class UserCreateRequest
    {
        public UserCreateRequest(string name)
        {
            Name = name;
        }

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
        /// An alias displayed to end users
        /// </summary>
        [JsonProperty("alias")]
        public string Alias { get; set; }

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
        /// The user's language identifier
        /// </summary>
        [JsonProperty("locale_id")]
        public long? LocaleId { get; set; }

        /// <summary>
        /// Designates whether the user has forum moderation capabilities
        /// </summary>
        [JsonProperty("moderator")]
        public bool? Moderator { get; set; }

        /// <summary>
        /// Any notes you want to store about the user
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// true if the user can only create private comments
        /// </summary>
        [JsonProperty("only_private_comments")]
        public bool? OnlyPrivateComments { get; set; }

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

        /// <summary>
        /// If the agent has any restrictions; false for admins and unrestricted agents, true for other agents
        /// </summary>
        [JsonProperty("restricted_agent")]
        public bool? RestrictedAgent { get; set; }

        /// <summary>
        /// The user's role. Possible values are "end-user", "agent", or "admin"
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }

        /// <summary>
        /// The user's signature. Only agents and admins can have signatures
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// If the agent is suspended. Tickets from suspended users are also suspended, and these users cannot sign in to the end user portal
        /// </summary>
        [JsonProperty("suspended")]
        public bool? Suspended { get; set; }

       [JsonProperty("shared_phone_number")]
        public bool? SharedPhoneNumber { get; set; }

        /// <summary>
        /// The user's tags. Only present if your account has user tagging enabled
        /// </summary>
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// Specifies which tickets the user has access to. Possible values are: "organization", "groups", "assigned", "requested", null
        /// </summary>
        [JsonProperty("ticket_restriction")]
        public string TicketRestriction { get; set; }

        /// <summary>
        /// The user's time zone
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
       

        //TODO: Is object correct??
        /// <summary>
        /// Values of custom fields in the user's profile
        /// </summary>
        [JsonProperty("user_fields")]
        public IDictionary<string, object> UserFields { get; set; }

        /// <summary>
        /// If the user's identity has been verified or not
        /// </summary>
        [JsonProperty("verified")]
        public bool? Verified { get; set; }
    }
}