using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum ComponentImpact
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
}