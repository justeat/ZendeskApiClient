using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class HelpCenterSection
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("outdated")]
        public bool? Outdated { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("category_id")]
        public long? CategoryId { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("source_locale")]
        public string SourceLocale { get; set; }

        [JsonProperty("user_segment_id")]
        public long? UserSegmentId { get; set; }

        [JsonProperty("parent_section_id")]
        public long? ParentSectionId { get; set; }

        [JsonProperty("manageable_by")]
        public string ManageableBy { get; set; }
    }
}
