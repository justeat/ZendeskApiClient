using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi.Client.Models.Status.Included;

public class IncludedReference
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public Type Type { get; set; }
}