using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models.Tickets
{
    public enum TicketType
    {
        [EnumMember(Value = null)]
        None,
        [EnumMember(Value = "task")]
        Task,
        [EnumMember(Value = "incident")]
        Incident,
        [EnumMember(Value = "problem")]
        Problem,
        [EnumMember(Value = "question")]
        Question
    }
}
