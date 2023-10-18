using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status;

public class Relationships
{
    [JsonProperty("incident_services")]
    public IncidentServices IncidentServices { get; set; }

    [JsonProperty("incident_updates")]
    public IncidentUpdates IncidentUpdates { get; set; }

    [JsonProperty("service")]
    public Service Service { get; set; }
}