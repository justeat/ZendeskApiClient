using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status;

public class Incident
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; }

    [JsonProperty("relationships")]
    public Relationships Relationships { get; set; }
}