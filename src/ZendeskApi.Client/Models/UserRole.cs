using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum UserRole
    {
        [EnumMember(Value = "end-user")]
        EndUser,
        [EnumMember(Value = "agent")]
        Agent,
        [EnumMember(Value = "admin")]
        Admin
    }
}