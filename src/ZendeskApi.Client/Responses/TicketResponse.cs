using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [SearchResultType("ticket")]
    public class TicketResponse : ISearchResult
    {
        /// <summary>
        /// Automatically assigned when creating tickets
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The API url of this ticket
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// An id you can use to link Zendesk Support tickets to local records
        /// </summary>
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        /// <summary>
        /// The type of this ticket, i.e. "problem", "incident", "question" or "task"
        /// </summary>
        [JsonProperty("type")]
        public TicketType? Type { get; set; }

        /// <summary>
        /// The value of the subject field for this ticket
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 	The dynamic content placeholder, if present, or the "subject" value, if not. See <see href="https://developer.zendesk.com/rest_api/docs/core/dynamic_content.html">Dynamic Content</see>
        /// </summary>
        [JsonProperty("raw_subject")]
        public string RawSubject { get; set; }

        /// <summary>
        /// The first comment on the ticket
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Priority, defines the urgency with which the ticket should be addressed: "urgent", "high", "normal", "low"
        /// </summary>
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }

        /// <summary>
        /// The state of the ticket, "new", "open", "pending", "hold", "solved", "closed"
        /// </summary>
        [JsonProperty("status")]
        public TicketStatus Status { get; set; }

        /// <summary>
        /// The original recipient e-mail address of the ticket
        /// </summary>
        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// The user who requested this ticket
        /// </summary>
        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        /// <summary>
        /// The user who submitted the ticket; The submitter always becomes the author of the first comment on the ticket.
        /// </summary>
        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        /// <summary>
        /// What agent is currently assigned to the ticket
        /// </summary>
        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        /// <summary>
        /// The organization of the requester. You can only specify the ID of an organization associated with the requester. See <see href="https://developer.zendesk.com/rest_api/docs/core/organization_memberships">Organization Memberships</see>
        /// </summary>
        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        /// <summary>
        /// The group this ticket is assigned to
        /// </summary>
        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        /// <summary>
        /// Who are currently CC'ed on the ticket
        /// </summary>
        [JsonProperty("collaborator_ids")]
        public IList<long> CollaboratorIds { get; set; }

        /// <summary>
        /// The topic this ticket originated from, if any
        /// </summary>
        [JsonProperty("forum_topic_id")]
        public long? ForumTopicId { get; set; }

        /// <summary>
        /// The problem this incident is linked to, if any
        /// </summary>
        [JsonProperty("problem_id")]
        public long? ProblemId { get; set; }

        /// <summary>
        /// Is true of this ticket has been marked as a problem, false otherwise
        /// </summary>
        [JsonProperty("has_incidents")]
        public bool HasIncidents { get; set; }

        /// <summary>
        /// If this is a ticket of type "task" it has a due date. Due date format uses ISO 8601 format.
        /// </summary>
        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        /// <summary>
        /// The array of tags applied to this ticket
        /// </summary>
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// This object explains how the ticket was created
        /// </summary>
        [JsonProperty("via")]
        public Via Via { get; set; }

        /// <summary>
        /// The custom fields of the ticket
        /// </summary>
        [JsonProperty("custom_fields")]
        [JsonConverter(typeof(CustomFieldsConverter))]
        public ICustomFields CustomFields { get; set; }

        /// <summary>
        /// The satisfaction rating of the ticket, if it exists, or the state of satisfaction, 'offered' or 'unoffered'
        /// </summary>
        [JsonProperty("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }

        /// <summary>
        /// The ids of the sharing agreements used for this ticket
        /// </summary>
        [JsonProperty("sharing_agreement_ids")]
        public IList<long> SharingAgreementIds { get; set; }

        /// <summary>
        /// The ids of the followups created from this ticket - only applicable for closed tickets
        /// </summary>
        [JsonProperty("followup_ids")]
        public IList<long> FollowupIds { get; set; }

        /// <summary>
        /// The id of the ticket form to render for this ticket - only applicable for enterprise accounts
        /// </summary>
        [JsonProperty("ticket_form_id")]
        public long? FormId { get; set; }

        /// <summary>
        /// The id of the brand this ticket is associated with - only applicable for enterprise accounts
        /// </summary>
        [JsonProperty("brand_id")]
        public long? BrandId { get; set; }

        /// <summary>
        /// Is false if channelback is disabled, true otherwise - only applicable for channels framework ticket
        /// </summary>
        [JsonProperty("allow_channelback")]
        public bool AllowChannelback { get; set; }

        /// <summary>
        /// Is true if any comments are public, false otherwise
        /// </summary>
        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// When this record was created
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When this record last got updated
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }


        [JsonProperty("result_type")]
        internal string ResultType => "ticket";
    }
}
