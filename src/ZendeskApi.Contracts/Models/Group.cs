using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [Description("Group")]
    [DataContract]
    public class Group : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "deleted", EmitDefaultValue = false)]
        public bool Deleted { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public Uri Url { get; set; }

        [DataMember(Name = "has_incidents", EmitDefaultValue = false)]
        public bool HasIncidents { get; set; }
    }
}
