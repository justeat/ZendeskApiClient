using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class LocaleListResponse
    {
        [JsonProperty("locales")]
        public Locale[] Locales { get; set; }
    }

    public class LocaleResponse
    {
        [JsonProperty("locale")]
        public Locale Locale { get; set; }
    }

    public class Locale
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime? Created { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("native_name")]
        public string NativeName { get; set; }

        [JsonProperty("presentation_name")]
        public string PresentationName { get; set; }

        [JsonProperty("locale")]
        public string Value { get; set; }

        [JsonProperty("rtl")]
        public bool Rtl { get; set; }
    }
}
