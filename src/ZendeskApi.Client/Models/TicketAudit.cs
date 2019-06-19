using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models.AuditEvents;

namespace ZendeskApi.Client.Models
{
    [JsonObject]
    public class TicketAudit
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("ticket_id")]
        public long TicketId { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("author_id")]
        public long AuthorId { get; set; }
        
        [JsonProperty("events")]
        [JsonConverter(typeof(AuditEventJsonConverter))]
        public IEnumerable<IAuditEvent> Events { get; set; }

        [JsonIgnore]
        public IEnumerable<CreateEvent> CreateEvents => Events.OfType<CreateEvent>();

        [JsonIgnore]
        public IEnumerable<ChangeEvent> ChangeEvents => Events.OfType<ChangeEvent>();

        [JsonIgnore]
        public IEnumerable<CommentEvent> CommentEvents => Events.OfType<CommentEvent>();

        [JsonIgnore]
        public IEnumerable<CommentRedactionEvent> CommentRedactionEvents => Events.OfType<CommentRedactionEvent>();

        [JsonIgnore]
        public IEnumerable<AttachmentRedactionEvent> AttachmentRedactionEvents =>
            Events.OfType<AttachmentRedactionEvent>();

        [JsonIgnore]
        public IEnumerable<VoiceCommentEvent> VoiceCommentEvents => Events.OfType<VoiceCommentEvent>();

        [JsonIgnore]
        public IEnumerable<CommentPrivacyChangeEvent> CommentPrivacyChangeEvents =>
            Events.OfType<CommentPrivacyChangeEvent>();

        [JsonIgnore]
        public IEnumerable<NotificationEvent> NotificationEvents => Events.OfType<NotificationEvent>();

        [JsonIgnore]
        public IEnumerable<NotificationWithCCsEvent> NotificationWithCCsEvents =>
            Events.OfType<NotificationWithCCsEvent>();

        [JsonIgnore]
        public IEnumerable<CCEvent> CcEvents => Events.OfType<CCEvent>();

        [JsonIgnore]
        public IEnumerable<FollowerNotificationEvent> FollowerNotificationEvents =>
            Events.OfType<FollowerNotificationEvent>();

        [JsonIgnore]
        public IEnumerable<FollowerChangeEvent> FollowerChangeEvents => Events.OfType<FollowerChangeEvent>();

        [JsonIgnore]
        public IEnumerable<EmailCCChangeEvent> EmailCcChangeEvents => Events.OfType<EmailCCChangeEvent>();

        [JsonIgnore]
        public IEnumerable<SatisfactionRatingEvent> SatisfactionRatingEvents =>
            Events.OfType<SatisfactionRatingEvent>();

        [JsonIgnore]
        public IEnumerable<TicketSharingEvent> TicketSharingEvents => Events.OfType<TicketSharingEvent>();

        [JsonIgnore]
        public IEnumerable<OrganizationActivityEvent> OrganizationActivityEvents =>
            Events.OfType<OrganizationActivityEvent>();

        [JsonIgnore]
        public IEnumerable<ErrorEvent> ErrorEvents => Events.OfType<ErrorEvent>();

        [JsonIgnore]
        public IEnumerable<TweetEvent> TweetEvents => Events.OfType<TweetEvent>();

        [JsonIgnore]
        public IEnumerable<FacebookEvent> FacebookEvents => Events.OfType<FacebookEvent>();

        [JsonIgnore]
        public IEnumerable<FacebookCommentEvent> FacebookCommentEvents => Events.OfType<FacebookCommentEvent>();

        [JsonIgnore]
        public IEnumerable<ExternalEvent> ExternalEvents => Events.OfType<ExternalEvent>();

        [JsonIgnore]
        public IEnumerable<LogMeInTranscriptEvent> LogMeInTranscriptEvents => Events.OfType<LogMeInTranscriptEvent>();

        [JsonIgnore]
        public IEnumerable<PushEvent> PushEvents => Events.OfType<PushEvent>();

    }
}