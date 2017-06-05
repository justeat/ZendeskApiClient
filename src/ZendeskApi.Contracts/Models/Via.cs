using Newtonsoft.Json;

namespace ZendeskApi.Contracts.Models
{
    public class Via
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }
    
    public class Source
    {
        [JsonProperty("to")]
        public dynamic To { get; set; }

        [JsonProperty("from")]
        public dynamic From { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }
    }
}
