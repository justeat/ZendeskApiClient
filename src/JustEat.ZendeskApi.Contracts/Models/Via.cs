using System.Runtime.Serialization;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Via
    {
        [DataMember(Name = "channel")]
        public string Channel { get; set; }
    }
}