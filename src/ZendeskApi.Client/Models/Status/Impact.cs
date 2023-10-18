using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models.Status;

public enum Impact
{
    [EnumMember(Value = "no impact")]
    NoImpact,
    [EnumMember(Value = "minor")]
    Minor,
    [EnumMember(Value = "major")]
    Major,
    [EnumMember(Value = "critical")]
    Critical,
    [EnumMember(Value = "security")]
    Security
}