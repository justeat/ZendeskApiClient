using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Contracts.Models
{
    [Description("Ticket")]
    [DataContract]
    public class Ticket : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "due_at", EmitDefaultValue = false)]
        public DateTime? Due { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public TicketType? Type { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "comment")]
        public TicketComment Comment { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public TicketStatus Status { get; set; }

        [DataMember(Name = "requester_id", EmitDefaultValue = false)]
        public long? RequesterId { get; set; }

        [DataMember(Name = "submitter_id", EmitDefaultValue = false)]
        public long? SubmitterId { get; set; }

        [DataMember(Name = "assignee_id", EmitDefaultValue = false)]
        public long? AssigneeId { get; set; }

        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public long? OrganisationId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Uri Url { get; set; }

        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public string Priority { get; set; }

        [DataMember(Name = "recipient", EmitDefaultValue = false)]
        public object Recipient { get; set; }

        [DataMember(Name = "group_id", EmitDefaultValue = false)]
        public int? GroupId { get; set; }

        [DataMember(Name = "ticket_form_id", EmitDefaultValue = false)]
        public int? FormId { get; set; }

        [DataMember(Name = "via")]
        public Via Via { get; set; }

        [DataMember(Name = "custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [DataMember(Name = "satisfaction_rating")]
        public object SatisfactionRating { get; set; }

        // ReSharper disable InconsistentNaming
        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public long? External_Id { get; set; }

        [IgnoreDataMember]
        public List<long> collaborator_ids { get; set; }

        [IgnoreDataMember]
        public long forum_topic_id { get; set; }

        [IgnoreDataMember]
        public long problem_id { get; set; }

        [IgnoreDataMember]
        public bool has_incidents { get; set; }

        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public List<string> Tags { get; set; }

        [IgnoreDataMember]
        public List<object> sharing_agreement_ids { get; set; }

        [IgnoreDataMember]
        public List<object> fields { get; set; }

        [IgnoreDataMember]
        public string result_type { get; set; }

        [IgnoreDataMember]
        public List<long> followup_ids { get; set; }

        [DataMember(Name = "requester")]
        public TicketRequester Requester { get; set; }
// ReSharper restore InconsistentNaming
    }
}
