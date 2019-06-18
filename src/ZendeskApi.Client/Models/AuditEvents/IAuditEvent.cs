using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public interface IAuditEvent
    {
        int Id { get; set; }
        AuditTypes Type { get; set; }
    }
}