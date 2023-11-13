using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.Status.Response;

public class ListIncidentsResponse : DataContainer<IReadOnlyList<Incident>>
{
    [JsonProperty("included")]
    public IReadOnlyList<Included.Included> Included { get; set; }
}