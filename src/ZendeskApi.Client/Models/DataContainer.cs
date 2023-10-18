using Newtonsoft.Json;

namespace ZendeskApi.Client.Models;

public class DataContainer<T>
{
    [JsonProperty("data")]
    public T Data { get; set; }
}