﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Client.Models
{
    [Description("Ticket")]
    public class Ticket
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public TicketType? Type { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("comment")]
        public TicketComment Comment { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("status")]
        public TicketStatus Status { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonProperty()]
        public Uri Url { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("recipient")]
        public object Recipient { get; set; }

        [JsonProperty("group_id")]
        public int? GroupId { get; set; }

        [JsonProperty("ticket_form_id")]
        public int? FormId { get; set; }

        [JsonProperty("via")]
        public Via Via { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("satisfaction_rating")]
        public object SatisfactionRating { get; set; }
        
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }
        
        [JsonProperty("collaborator_ids")]
        public List<long> CollaboratorIds { get; set; }

        [JsonIgnore]
        [JsonProperty("forum_topic_id")]
        public long ForumTopicId { get; set; }

        [JsonIgnore]
        [JsonProperty("problem_id")]
        public long ProblemId { get; set; }

        [JsonIgnore]
        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonIgnore]
        [JsonProperty("sharing_agreement_ids")]
        public List<object> SharingAgreementIds { get; set; }

        [JsonIgnore]
        [JsonProperty("fields")]
        public List<object> Fields { get; set; }

        [JsonIgnore]
        [JsonProperty("result_type")]
        public string ResultType { get; set; }

        [JsonIgnore]
        [JsonProperty("followup_ids")]
        public List<long> FollowupIds { get; set; }

        [JsonProperty("requester")]
        public TicketRequester Requester { get; set; }
    }
}