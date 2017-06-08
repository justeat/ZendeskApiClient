using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class TicketForm
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("raw_name")]
        public string RawName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("raw_display_name")]
        public string RawDisplayName { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("end_user_visible")]
        public bool? EndUserVisible { get; set; }

        [JsonProperty("ticket_field_ids")]
        public List<long> TicketFieldIds { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("default")]
        public bool? Default { get; set; }

    }
}
