using System;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status;

public class MaintenanceAttributes
{
    [JsonProperty("degradation")]
    public bool Degradation { get; set; }

    [JsonProperty("impact")]
    public Impact Impact { get; set; }

    [JsonProperty("outage")]
    public bool Outage { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("maintenance_article")]
    public string MaintenanceArticle { get; set; }

    [JsonProperty("maintenance_end_time")]
    public DateTime MaintenanceEndTime { get; set; }

    [JsonProperty("maintenance_start_time")]
    public DateTime MaintenanceStartTime { get; set; }
}