using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Tickets
{
    public abstract class BaseTicket
    {
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("type")]
        public TicketType Type { get; set; } = TicketType.None;

        [JsonProperty("priority")]
        public Models.Priority Priority { get; set; } = Models.Priority.None;

        [JsonProperty("status")]
        public TicketStatus Status { get; set; } = TicketStatus.None;

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("raw_subject")]
        public string RawSubject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        [JsonProperty("collaborator_ids")]
        public List<long> CollaboratorIds { get; set; }

        [JsonProperty("forum_topic_id")]
        public long? ForumTopicId { get; set; }

        [JsonProperty("problem_id")]
        public long? ProblemId { get; set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }

        [JsonProperty("sharing_agreement_ids")]
        public List<long> SharingAgreementIds { get; set; }

        [JsonProperty("followup_ids")]
        public List<long> FollowupIds { get; set; }
    }
}