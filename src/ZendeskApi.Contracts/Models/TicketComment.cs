using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Contracts.Models
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

        [DataMember(Name = "attachments")]
        public List<Attachment> Attachments { get; private set; }

        [DataMember(Name = "uploads")]
        public List<string> Uploads { get; set; }

        [DataMember(Name = "via")]
        public Via Via { get; set; }

        // ReSharper disable InconsistentNaming
        [IgnoreDataMember]
        public object metadata { get; set; }

        // ReSharper restore InconsistentNaming

        public void AddAttachmentToComment(string attachmentToken)
        {
            Uploads = Uploads ?? new List<string>();

            Uploads.Add(attachmentToken);
        }

    }
}