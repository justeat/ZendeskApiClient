using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [Description("Comment")]
    [DataContract]
    public class TicketComment : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(EmitDefaultValue = false)]
        public TicketEventType Type { get; set; }

        [DataMember(Name = "html_body")]
        public string HtmlBody { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "author_id")]
        public long? Author { get; set; }

        [DataMember(Name = "public")]
        public bool IsPublic { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }
        
// ReSharper disable InconsistentNaming
        [IgnoreDataMember]
        public object attachments { get; set; }

        [IgnoreDataMember]
        public object metadata { get; set; }

        [IgnoreDataMember]
        public object via { get; set; }
// ReSharper restore InconsistentNaming

    }
}
