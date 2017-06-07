using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    [Description("Ticket Field")]
    public class TicketField
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("raw_title")]
        public string RawTitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("raw_description")]
        public string RawDescription { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("required")]
        public bool? Required { get; set; }

        [JsonProperty("collapsed_for_agents")]
        public bool? CollapsedForAgents { get; set; }

        [JsonProperty("regexp_for_validation")]
        public string RegexpForValidation { get; set; }

        [JsonProperty("title_in_portal")]
        public string TitleInPortal { get; set; }

        [JsonProperty("raw_title_in_portal")]
        public string RawTitleInPortal { get; set; }

        [JsonProperty("visible_in_portal")]
        public bool? VisibleInPortal { get; set; }

        [JsonProperty("editable_in_portal")]
        public bool? EditableInPortal { get; set; }

        [JsonProperty("required_in_portal")]
        public bool? RequiredInPortal { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("removable")]
        public bool? Removable { get; set; }

        [JsonProperty("custom_field_options")]
        public List<CustomFieldOption> CustomFieldOptions { get; set; }
    }
}
