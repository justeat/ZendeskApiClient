using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ChangeEvent : AuditEvent, IAuditEvent
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }
        [JsonProperty("value")]
        public object Value { get; set; }
        [JsonProperty("previous_value")]
        public object PreviousValue { get; set; }
    }
}