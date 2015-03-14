using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [Description("User")]
    [DataContract]
    public class User : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "last_login_at", EmitDefaultValue = false)]
        public DateTime? LastLoginAt { get; set; }

        [DataMember(Name = "time_zone", EmitDefaultValue = false)]
        public string TimeZone { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        [DataMember(Name = "locale", EmitDefaultValue = false)]
        public string Locale { get; set; }

        [DataMember(Name = "locale_id", EmitDefaultValue = false)]
        public int? LocalId { get; set; }

        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public long? OrganizationId { get; set; }

        [DataMember(Name = "role", EmitDefaultValue = false)]
        public string Role { get; set; }

        [DataMember(Name = "verified", EmitDefaultValue = false)]
        public bool Verified { get; set; }

        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string ExternalId { get; set; }

        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public List<string> Tags { get; set; }

        [DataMember(Name = "active", EmitDefaultValue = false)]
        public bool Active { get; set; }

        [DataMember(Name = "shared", EmitDefaultValue = false)]
        public bool Shared { get; set; }

        [DataMember(Name = "shared_agent", EmitDefaultValue = false)]
        public bool SharedAgent { get; set; }

        [DataMember(Name = "signature", EmitDefaultValue = false)]
        public string Signature { get; set; }

        [DataMember(Name = "details", EmitDefaultValue = false)]
        public string Details { get; set; }

        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public string Notes { get; set; }

        [DataMember(Name = "custom_role_id", EmitDefaultValue = false)]
        public long? CustomRoleId { get; set; }

        [DataMember(Name = "moderator", EmitDefaultValue = false)]
        public bool Moderator { get; set; }

        [DataMember(Name = "only_private_comments", EmitDefaultValue = false)]
        public bool OnlyPrivateComments { get; set; }

        [DataMember(Name = "restricted_agent", EmitDefaultValue = false)]
        public bool RestrictedAgent { get; set; }

        [DataMember(Name = "suspended", EmitDefaultValue = false)]
        public bool Suspended { get; set; }

        [DataMember(Name = "ticket_restriction", EmitDefaultValue = false)]
        public string TicketRestriction { get; set; }

        [DataMember(Name = "user_fields", EmitDefaultValue = false)]
        public Dictionary<string, object> UserFields { get; set; }
    }
}
