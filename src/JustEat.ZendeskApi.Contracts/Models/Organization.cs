using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Organization : IZendeskEntity
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

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

// ReSharper disable InconsistentNaming       
        [IgnoreDataMember]
        public bool shared_tickets { get; set; }

        [IgnoreDataMember]
        public bool shared_comments { get; set; }

        [IgnoreDataMember]
        public List<string> Tags { get; set; }

        [IgnoreDataMember]
        public Uri Url { get; set; }

        [IgnoreDataMember]
        public long group_id { get; set; }

        [IgnoreDataMember]
        public long external_id { get; set; }

        [IgnoreDataMember]
        public object domain_names { get; set; }

// ReSharper restore InconsistentNaming
    }
}
