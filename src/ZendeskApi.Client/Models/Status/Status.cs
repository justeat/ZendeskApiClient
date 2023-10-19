using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models.Status;

public enum Status
{
    [EnumMember(Value = "operational")]
    Operational,
    [EnumMember(Value = "impeded")]
    Impeded,
    [EnumMember(Value = "inoperative")]
    Inoperative,
    [EnumMember(Value = "investigating")]
    Investigating
}