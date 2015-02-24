using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [Description("Attachment")]
    [DataContract]
    public class Attachment : Thumbnail
    {
        [DataMember(Name = "thumbnails", EmitDefaultValue = false)]
        public List<Thumbnail> Thumbnails { get; set; }
    }

    [DataContract]
    public class Thumbnail : IZendeskEntity
    {
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string Url { get; set; }

        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "file_name", EmitDefaultValue = false)]
        public string FileName { get; set; }

        [DataMember(Name = "content_url", EmitDefaultValue = false)]
        public string ContentUrl { get; set; }

        [DataMember(Name = "content_type", EmitDefaultValue = false)]
        public string ContentType { get; set; }

        [DataMember(Name = "size", EmitDefaultValue = false)]
        public int Size { get; set; }
    }
}
