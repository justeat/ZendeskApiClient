using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.AuditEvents;

namespace ZendeskApi.Client.Converters
{
    public class AuditEventJsonConverter : JsonConverter
    {
        private readonly Dictionary<AuditTypes, Type> _typeMappings = new Dictionary<AuditTypes, Type>
        {
            {AuditTypes.Create, typeof(CreateEvent)},
            {AuditTypes.Change, typeof(ChangeEvent)},
            {AuditTypes.Comment, typeof(CommentEvent)},
            {AuditTypes.CommentRedactionEvent, typeof(CommentRedactionEvent)},
            {AuditTypes.AttachmentRedactionEvent, typeof(AttachmentRedactionEvent)},
            {AuditTypes.VoiceComment, typeof(VoiceCommentEvent)},
            {AuditTypes.CommentPrivacyChange, typeof(CommentPrivacyChangeEvent)},
            {AuditTypes.Notification, typeof(NotificationEvent)},
            {AuditTypes.NotificationWithCcs, typeof(NotificationWithCCsEvent)},
            {AuditTypes.Cc, typeof(CCEvent)},
            {AuditTypes.FollowerNotificationEvent, typeof(FollowerNotificationEvent)},
            {AuditTypes.FollowersChange, typeof(FollowerChangeEvent)},
            {AuditTypes.EmailCcChange, typeof(EmailCCChangeEvent)},
            {AuditTypes.SatisfactionRating, typeof(SatisfactionRatingEvent)},
            {AuditTypes.TicketSharingEvent, typeof(TicketSharingEvent)},
            {AuditTypes.OrganizationActivity, typeof(OrganizationActivityEvent)},
            {AuditTypes.Error, typeof(ErrorEvent)},
            {AuditTypes.Tweet, typeof(TweetEvent)},
            {AuditTypes.FacebookEvent, typeof(FacebookEvent)},
            {AuditTypes.FacebookComment, typeof(FacebookCommentEvent)},
            {AuditTypes.External, typeof(ExternalEvent)},
            {AuditTypes.LogMeInTranscript, typeof(LogMeInTranscriptEvent)},
            {AuditTypes.Push, typeof(PushEvent)}
        };
        
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<IAuditEvent>) ||
                   objectType == typeof(JObject);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                {
                    JObject item = JObject.Load(reader);
                    if (item["type"] != null && Enum.TryParse(item["type"].ToString(), out AuditTypes type))
                    {
                        return item.ToObject(_typeMappings[type]);
                    }
                    break;
                }
            }

            return null;
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}