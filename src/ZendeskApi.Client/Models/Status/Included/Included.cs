using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status.Included;

public class Included
{
    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public Type Type { get; set; }

    [JsonProperty("relationships")]
    public Relationships Relationships { get; set; }
}