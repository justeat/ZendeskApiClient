using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status.Included;

public class Relationships
{
    [JsonProperty("service")]
    public Service Service { get; set; }
}