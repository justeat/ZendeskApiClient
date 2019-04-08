using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    public class Article : ISearchResult
    {
        /// <summary>
        /// Automatically assigned when creating articles
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The API url of this article
        /// </summary>
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("source_locale")]
        public string SourceLocale { get; set; }

        [JsonProperty("author_id")]
        public long? Author { get; set; }

        [JsonProperty("comments_disabled")]
        public bool? CommentsDisabled { get; set; }

        [JsonProperty("outdated_locales")]
        public List<string> OutdatedLocales { get; set; }

        [JsonProperty("label_names")]
        public List<string> LabelNames { get; set; }

        [JsonProperty("draft")]
        public bool? Draft { get; set; }

        [JsonProperty("promoted")]
        public bool? Promoted { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("vote_sum")]
        public int? VoteSum { get; set; }

        [JsonProperty("vote_count")]
        public int? VoteCount { get; set; }

        [JsonProperty("section_id")]
        public long? Section { get; set; }

        [JsonProperty("user_segment_id")]
        public long? UserSegment { get; set; }

        [JsonProperty("permission_group_id")]
        public int PermissionGroup { get; set; }

        /// <summary>
        /// When this record was created
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("edited_at")]
        public DateTime EditedAt { get; set; }

        /// <summary>
        /// When this record last got updated
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}
