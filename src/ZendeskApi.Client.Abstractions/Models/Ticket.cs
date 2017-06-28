using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Client.Models
{
    [JsonObject("ticket")]
    public class Ticket : ISearchResult
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty]
        public Uri Url { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public TicketType? Type { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("raw_subject")]
        public string RawSubject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        // TODO: Enum
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public TicketStatus Status { get; set; }

        [JsonProperty("recipient")]
        public object Recipient { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        [JsonProperty("collaborator_ids")]
        public List<long> CollaboratorIds { get; set; }

        [JsonProperty("forum_topic_id")]
        public long? ForumTopicId { get; set; }

        [JsonProperty("problem_id")]
        public long? ProblemId { get; set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }

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

        [JsonProperty("ticket_form_id")]
        public long? FormId { get; set; }

        [JsonProperty("brand_id")]
        public long? BrandId { get; set; }

        [JsonProperty("allow_channelback")]
        public bool AllowChannelback { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        
        // Requred for Create
        [JsonProperty("comment")]
        public TicketComment Comment { get; set; }

        // Requred for Create
        [JsonProperty("requester")]
        public TicketRequester Requester { get; set; }


        DateTime ISearchResult.CreatedAt => CreatedAt.Value;
        DateTime ISearchResult.UpdatedAt => UpdatedAt.Value;
        long ISearchResult.Id => Id.Value;
        Uri ISearchResult.Url => Url;

        [JsonProperty("result_type")]
        string ISearchResult.Type => typeof(Ticket).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}
