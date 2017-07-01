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
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public TicketType? Type { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public TicketStatus Status { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

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
        public List<string> Tags { get; set; } = new List<string>();

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>();
        
        [JsonProperty("sharing_agreement_ids")]
        public List<long> SharingAgreementIds { get; set; } = new List<long>();

        [JsonProperty("ticket_form_id")]
        public long? FormId { get; set; }



        // Requred for Create and Update
        [JsonProperty("comment")]
        public TicketComment Comment { internal get; set; }
        [JsonProperty("requester")]
        public TicketRequester Requester { internal get; set; }

        // Required For Retrieve
        [JsonProperty]
        public long? Id { get; internal set; }

        [JsonProperty]
        public Uri Url { get; internal set; }

        [JsonProperty("raw_subject")]
        public string RawSubject { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("recipient")]
        public string Recipient { get; internal set; }

        [JsonProperty("organization_id")]
        public long? OrganisationId { get; internal set; }

        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; internal set; }

        [JsonProperty("via")]
        public Via Via { get; private set; }

        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; internal set; }

        [JsonProperty("followup_ids")]
        public List<long> FollowupIds { get; internal set; } = new List<long>();

        [JsonProperty("brand_id")]
        public long? BrandId { get; internal set; }

        [JsonProperty("allow_channelback")]
        public bool AllowChannelback { get; internal set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; internal set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; internal set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; internal set; }


        DateTime ISearchResult.CreatedAt => CreatedAt.Value;
        DateTime ISearchResult.UpdatedAt => UpdatedAt.Value;
        long ISearchResult.Id => Id.Value;
        Uri ISearchResult.Url => Url;

        [JsonProperty("result_type")]
        string ISearchResult.Type => typeof(Ticket).GetTypeInfo().GetCustomAttribute<JsonObjectAttribute>().Id;
    }
}
