using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZendeskApi.Client.Models
{
    [JsonObject("comment")]
    public class TicketComment
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty()]
        public TicketEventType Type { get; set; }

        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("author_id")]
        public long? Author { get; set; }

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; private set; }

        [JsonProperty("uploads")]
        public List<string> Uploads { get; set; }


        [JsonProperty("via")]
        public Via Via { get; set; }
        public void AddAttachmentToComment(string attachmentToken)
        {
            Uploads = Uploads ?? new List<string>();

            Uploads.Add(attachmentToken);
        }

    }
}