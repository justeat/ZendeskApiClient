using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models.Status.Included;

public enum Type
{
    [EnumMember(Value = "service")]
    Service,
    [EnumMember(Value = "incident_service")]
    IncidentService,
    [EnumMember(Value = "incident_update")]
    IncidentUpdate,
    [EnumMember(Value = "incident")]
    Incident
}