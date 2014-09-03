using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [DataContract]
    public enum TicketStatus
    {
        [EnumMember(Value = "new")]
        New,
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "closed")]
        Closed,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "solved")]
        Solved
    }
}