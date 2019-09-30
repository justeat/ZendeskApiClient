using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class HelpCenterLocales
    {
        [JsonProperty("default_locale")]
        public string DefaultLocale { get; set; }

        [JsonProperty("locales")]
        public string[] Locales { get; set; }
    }
}
