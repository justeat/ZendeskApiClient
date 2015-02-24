using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Upload : IZendeskEntity
    {
        [IgnoreDataMember]
        public long? Id { get; set; }

        [DataMember(Name = "token", EmitDefaultValue = false)]
        public string Token { get; set; }

        [DataMember(Name = "expires_at", EmitDefaultValue = false)]
        public DateTime ExpiresAt { get; set; }

        [DataMember(Name = "attachment", EmitDefaultValue = false)]
        public Attachment Attachment { get; set; }
    }
}