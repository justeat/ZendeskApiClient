using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public interface IAuditEvent
    {
        [JsonProperty("id")]
        long Id { get; set; }
        [JsonProperty("type")]
        AuditTypes Type { get; set; }
    }
}