using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [Description("Ticket Field")]
    [DataContract]
    public class TicketField : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public Uri Url { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "raw_title", EmitDefaultValue = false)]
        public string RawTitle { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "raw_description", EmitDefaultValue = false)]
        public string RawDescription { get; set; }

        [DataMember(Name = "position", EmitDefaultValue = false)]
        public int? Position { get; set; }

        [DataMember(Name = "active", EmitDefaultValue = false)]
        public bool? Active { get; set; }

        [DataMember(Name = "required", EmitDefaultValue = false)]
        public bool? Required { get; set; }

        [DataMember(Name = "collapsed_for_agents", EmitDefaultValue = false)]
        public bool? CollapsedForAgents { get; set; }

        [DataMember(Name = "regexp_for_validation", EmitDefaultValue = false)]
        public string RegexpForValidation { get; set; }

        [DataMember(Name = "title_in_portal", EmitDefaultValue = false)]
        public string TitleInPortal { get; set; }

        [DataMember(Name = "raw_title_in_portal", EmitDefaultValue = false)]
        public string RawTitleInPortal { get; set; }

        [DataMember(Name = "visible_in_portal", EmitDefaultValue = false)]
        public bool? VisibleInPortal { get; set; }

        [DataMember(Name = "editable_in_portal", EmitDefaultValue = false)]
        public bool? EditableInPortal { get; set; }

        [DataMember(Name = "required_in_portal", EmitDefaultValue = false)]
        public bool? RequiredInPortal { get; set; }

        [DataMember(Name = "tag", EmitDefaultValue = false)]
        public string Tag { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? UpdatedAt { get; set; }

        [DataMember(Name = "removable", EmitDefaultValue = false)]
        public bool? Removable { get; set; }

    }
}
