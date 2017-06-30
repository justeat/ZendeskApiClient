using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum Priority
    {
        [EnumMember(Value = null)]
        None,
        [EnumMember(Value = "urgent")]
        Urgent,
        [EnumMember(Value = "high")]
        High,
        [EnumMember(Value = "normal")]
        Normal,
        [EnumMember(Value = "low")]
        Low
    }
}
