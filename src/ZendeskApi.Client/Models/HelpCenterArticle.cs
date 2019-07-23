using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class HelpCenterArticle
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("author_id")]
        public long? AuthorId { get; set; }

        [JsonProperty("comments_disabled")]
        public bool? CommentsDisabled { get; set; }

        [JsonProperty("outdated")]
        public bool? Outdated { get; set; }

        [JsonProperty("label_names")]
        public IEnumerable<string> LabelNames { get; set; }

        [JsonProperty("promoted")]
        public bool? Promoted { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("section_id")]
        public long? SectionId { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("source_locale")]
        public string SourceLocale { get; set; }

        [JsonProperty("draft")]
        public bool Draft { get; set; }
    }
}