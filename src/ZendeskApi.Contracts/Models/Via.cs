using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Via
    {
        [DataMember(Name = "channel")]
        public string Channel { get; set; }
    }
}