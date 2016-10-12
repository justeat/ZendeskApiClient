using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [Description("Organization")]
    [DataContract]
    public class Organization : IZendeskEntity
    {
        [DataMember(Name = "id")]
        public long? Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = true)]
        public string Name { get; set; }

        [DataMember(Name = "details", EmitDefaultValue = true)]
        public string Details { get; set; }

        [DataMember(Name = "notes", EmitDefaultValue = true)]
        public string Notes { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "due_at", EmitDefaultValue = false)]
        public DateTime? Due { get; set; }

        [DataMember(Name = "organization_fields", EmitDefaultValue = true)]
        public Dictionary<object, object> CustomFields { get; set; }
        
        [DataMember(Name = "tags", EmitDefaultValue = true)]
        public List<string> Tags { get; set; }
        
        [DataMember(Name = "external_id", EmitDefaultValue = false)]
        public string external_id { get; set; }

        [DataMember(Name = "domain_names", EmitDefaultValue = false)]
        public List<string> DomainNames { get; set; }

// ReSharper disable InconsistentNaming       
        [IgnoreDataMember]
        public bool shared_tickets { get; set; }

        [IgnoreDataMember]
        public bool shared_comments { get; set; }

        [IgnoreDataMember]
        public Uri Url { get; set; }

        [IgnoreDataMember]
        public long group_id { get; set; }
// ReSharper restore InconsistentNaming
    }
}
