using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Contracts.Models
{
    [Description("Request")]
    [DataContract]
    public class Request : IZendeskEntity
    {
        public Request()
        {
            CustomFields = new List<CustomField>();
        }
        
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public Uri Url { get; set; }
        
        [DataMember(Name = "description")]
        public string Description { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public TicketStatus Status { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "due_at", EmitDefaultValue = false)]
        public DateTime? Due { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TicketType? Type { get; set; }
        
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public string Priority { get; set; }

        [DataMember(Name = "comment")]
        public TicketComment Comment { get; set; }

        [DataMember(Name = "requester_id", EmitDefaultValue = false)]
        public long? RequesterId { get; set; }

        [DataMember(Name = "submitter_id", EmitDefaultValue = false)]
        public long? SubmitterId { get; set; }

        [DataMember(Name = "assignee_id", EmitDefaultValue = false)]
        public long? AssigneeId { get; set; }

        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public long? OrganisationId { get; set; }

        [DataMember(Name = "collaborator_ids", EmitDefaultValue = false)]
        public List<int> CollaboratorIds { get; set; }

        [DataMember(Name = "group_id", EmitDefaultValue = false)]
        public long? GroupId { get; set; }

        [DataMember(Name = "ticket_form_id", EmitDefaultValue = false)]
        public int? FormId { get; set; }

        [DataMember(Name = "can_be_solved_by_me")]
        public bool CanBeSolvedByMe { get; set; }

        [DataMember(Name = "solved")]
        public bool Solved { get; set; }

        [DataMember(Name = "via")]
        public Via Via { get; set; }

        [DataMember(Name = "custom_fields")]
        public List<CustomField> CustomFields { get; set; }
    }
}
