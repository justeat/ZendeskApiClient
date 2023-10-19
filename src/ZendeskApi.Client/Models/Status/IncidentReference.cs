using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status;

public class IncidentReference
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}