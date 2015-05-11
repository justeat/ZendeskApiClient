using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Contracts.Models
{
    [Description("satisfaction_rating")]
    [DataContract]
    public class SatisfactionRating : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "group_id", EmitDefaultValue = false)]
        public long GroupId { get; set; }

        [DataMember(Name = "assignee_id", EmitDefaultValue = false)]
        public long AssigneeId { get; set; }

        [DataMember(Name = "requester_id", EmitDefaultValue = false)]
        public long RequesterId { get; set; }

        [DataMember(Name = "ticket_id", EmitDefaultValue = false)]
        public long TicketId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "score", EmitDefaultValue = false)]
        public SatisfactionRatingScore Score { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "comment")]
        public TicketComment Comment { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }
    }
}
