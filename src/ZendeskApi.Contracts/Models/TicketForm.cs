using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [DataContract]
    [DebuggerDisplay("Id:{Id} Name:{Name}")]
    public class TicketForm : IZendeskEntity
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public Uri Url { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "raw_name", EmitDefaultValue = false)]
        public string RawName { get; set; }

        [DataMember(Name = "display_name", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        [DataMember(Name = "raw_display_name", EmitDefaultValue = false)]
        public string RawDisplayName { get; set; }

        [DataMember(Name = "position", EmitDefaultValue = false)]
        public long? Position { get; set; }

        [DataMember(Name = "end_user_visible", EmitDefaultValue = false)]
        public bool? EndUserVisible { get; set; }

        [DataMember(Name = "ticket_field_ids", EmitDefaultValue = false)]
        public List<long> TicketFieldIds { get; set; }

        [DataMember(Name = "active", EmitDefaultValue = false)]
        public bool? Active { get; set; }

        [DataMember(Name = "default", EmitDefaultValue = false)]
        public bool? Default { get; set; }

    }
}
