using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Contracts.Models
{
    [Description("Request")]
    public class Request
    {
        public Request()
        {
            CustomFields = new List<CustomField>();
        }
        
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
        
        [JsonProperty("url")]
        public Uri Url { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public TicketStatus Status { get; set; }

        [JsonProperty("created_at")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public TicketType? Type { get; set; }
        
        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("comment")]
        public TicketComment Comment { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonProperty("collaborator_ids")]
        public List<int> CollaboratorIds { get; set; }

        [JsonProperty("group_id")]
        public int? GroupId { get; set; }

        [JsonProperty("ticket_form_id")]
        public int? FormId { get; set; }

        [JsonProperty("can_be_solved_by_me")]
        public bool CanBeSolvedByMe { get; set; }

        [JsonProperty("solved")]
        public bool Solved { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }
    }
}
