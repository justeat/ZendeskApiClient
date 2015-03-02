using System.Runtime.Serialization;

namespace ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Via
    {
        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "source")]
        public Source Source { get; set; }
    }

    public class Source
    {
        [DataMember(Name = "to")]
        public object To { get; set; }

        [DataMember(Name = "rel")]
        public string Rel { get; set; }
    }
}
