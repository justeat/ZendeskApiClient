using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject("ticket")]
    public class TicketResponse : ISearchResult
    {
        [JsonProperty]
        public long Id { get; internal set; }

        [JsonProperty]
        public Uri Url { get; internal set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; internal set; }
        
        [JsonProperty("type")]
        public TicketType? Type { get; internal set; }

        [JsonProperty("subject")]
        public string Subject { get; internal set; }

        [JsonProperty("raw_subject")]
        public string RawSubject { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }
        
        [JsonProperty("priority")]
        public Priority? Priority { get; internal set; }
        
        [JsonProperty("status")]
        public TicketStatus Status { get; internal set; }

        [JsonProperty("recipient")]
        public object Recipient { get; internal set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; internal set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; internal set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; internal set; }

        [JsonProperty("organization_id")]
        public long? OrganisationId { get; internal set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; internal set; }

        [JsonProperty("collaborator_ids")]
        public IReadOnlyList<long> CollaboratorIds { get; internal set; }

        [JsonProperty("forum_topic_id")]
        public long? ForumTopicId { get; internal set; }

        [JsonProperty("problem_id")]
        public long? ProblemId { get; internal set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; internal set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; internal set; }

        [JsonProperty("tags")]
        public IReadOnlyList<string> Tags { get; internal set; }

        [JsonProperty("via")]
        public Via Via { get; internal set; }

        [JsonProperty("custom_fields")]
        public IReadOnlyList<CustomField> CustomFields { get; internal set; }

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; internal set; }

        [JsonProperty("sharing_agreement_ids")]
        public IReadOnlyList<long> SharingAgreementIds { get; internal set; }

        [JsonProperty("followup_ids")]
        public IReadOnlyList<long> FollowupIds { get; internal set; }

        [JsonProperty("ticket_form_id")]
        public long? FormId { get; internal set; }

        [JsonProperty("brand_id")]
        public long? BrandId { get; internal set; }

        [JsonProperty("allow_channelback")]
        public bool AllowChannelback { get; internal set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; internal set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; internal set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; internal set; }
        
        [JsonProperty("result_type")]
        string ISearchResult.Type => typeof(TicketResponse).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}
